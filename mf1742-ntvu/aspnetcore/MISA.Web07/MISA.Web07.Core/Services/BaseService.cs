using MISA.Web07.Core.Entities;
using MISA.Web07.Core.Exceptions;
using MISA.Web07.Core.Interfaces.Infrastructure;
using MISA.Web07.Core.Interfaces.MAttributes;
using MISA.Web07.Core.Interfaces.Services;
using MISA.Web07.Core.MAttributes;
using MISA.Web07.Core.Resources;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.Core.Services
{
    public class BaseService<T, TInsertModel, TViewModel> : IBaseService<T, TInsertModel> where T : BaseEntity where TInsertModel : T
    {
        #region Fields

        private readonly IBaseRepository<T> _baseRepository;
        #endregion

        #region Constructor

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;

        }
        #endregion

        #region Methods

        /// <summary>
        /// Validate dữ liệu, nếu thành công thì cho repo insert vào DB
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created: NTVU - 05/09/2023
        public async Task<int> InsertAsync(TInsertModel entity)
        {
            Validate(entity);
            await SelfValidateAsync(entity, Guid.Empty);

            return await _baseRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Validate dữ liệu, nếu thành công thì cho repo update vào DB
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created: NTVU - 05/09/2023

        public async Task<int> UpdateAsync(TInsertModel entity, Guid entityId)
        {

            var entityInDb = await _baseRepository.GetByIdAsync(entityId);
            if (entityInDb != null)
            {
                if (entity.ModifiedDate != entityInDb.ModifiedDate)
                {
                    throw new MISAUpdateException(Resources.ResourceVN.Error_ModifiedAsync);
                }
                else
                {
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = "NTVu";

                    if (Validate(entity) && await SelfValidateAsync(entity, entityId))
                    {
                        return await _baseRepository.UpdateAsync(entity, entityId);
                    }
                    else
                    {
                        return 0;
                    }

                }


            }
            else
            {
                throw new MISANotFoundException(ResourceVN.Error_NotFound);
            }

        }

        /// <summary>
        /// Validate dữ liệu theo attrs custome
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool Validate(T entity)
        {

            bool isValid = true;

            var props = entity.GetType().GetProperties();
            var errorList = new Dictionary<string, string>();
            foreach (var prop in props)
            {
                var customeAttrs = prop.GetCustomAttributes();
                var displayName = prop.Name;
                var propValue = prop.GetValue(entity, null);
                // Kiểm tra value prop là null, nếu ko yêu cầu not empty thì bỏ qua kiểm tra
                if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                {
                    var notEmptyRule = customeAttrs.FirstOrDefault(attr => attr.GetType() == typeof(NotEmpty));
                    if (notEmptyRule == null)
                    {
                        continue;
                    }
                }
                else
                {
                    // Lặp toàn bộ custome Attr
                    foreach (var attr in customeAttrs)
                    {
                        // Kiểm tra attr có phải luật không
                        var attrRuler = attr as IRuleAttributes;
                        if (attrRuler != null)
                        {
                            // Thực hiện kiểm tra, nếu đúng thì thêm lỗi
                            // sau đó continue đến prop kế tiếp
                            if (!attrRuler.Validate(value: propValue.ToString()))
                            {

                                var displayClass = (PropertyName[])prop.GetCustomAttributes(typeof(PropertyName));
                                if (displayClass.Length > 0)
                                {
                                    displayName = displayClass[0].Name;
                                }
                                isValid = false;
                                errorList.Add(prop.Name, attrRuler.GetErrorMessage(displayName));
                                break;
                            }
                        }
                    }
                }

            }

            if (!isValid)
            {
                var error = new MISAValidateException(errorList.First().Value)
                {
                    
                    Data = errorList
                };
                throw error;
            }
            return true;
        }

        /// <summary>
        /// Validate riêng
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created: NTVu - 05/09/2023
        protected virtual async Task<bool> SelfValidateAsync(TInsertModel entity, Guid entityId)
        {
            await Task.Yield(); // Tránh cảnh báo
            return true;
        }

        /// <summary>
        /// Export to Excel file
        /// </summary>
        /// <returns>
        /// File excel
        /// </returns>
        public async Task<MemoryStream> ExportToExcelAsync()
        {
            var title = $"{typeof(T).Name} EXCEL";

            var data = (await _baseRepository.GetAllAsync<TViewModel>()).ToList();

            var props = typeof(TViewModel).GetProperties();


            // Tạo một tệp Excel mới
            using var package = new ExcelPackage();
            // Tạo một sheet trong tệp Excel
            var worksheet = package.Workbook.Worksheets.Add(title);


            // CSS Background cho header

            var headerRow = worksheet.Row(4);

            headerRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

            headerRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);

            worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;


            // Thêm cột STT
            worksheet.Cells[4, 1].Value = "STT";

            int i = 2;
            foreach (var prop in props)
            {
                // Lấy tên cột
                var displayName = prop.Name;
                var displayClass = (PropertyName[])prop.GetCustomAttributes(typeof(PropertyName));

                if (displayClass.Length > 0)
                {
                    displayName = displayClass[0].Name;
                }

                worksheet.Cells[4, i].Value = displayName;

                var maxLengthClasses = (MaxLength[])prop.GetCustomAttributes(typeof(MaxLength));

                if (maxLengthClasses.Length > 0)
                {
                    var width = maxLengthClasses[0].Length;
                    if (width >= 100)
                    {
                        worksheet.Column(i).Width = 50;
                    }
                    else
                    {
                        worksheet.Column(i).Width = width;
                    }

                }

                // Lấy format cột
                Type propType = prop.PropertyType;
                if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                {
                    worksheet.Column(i).Style.Numberformat.Format = "yyyy-MM-dd";
                    worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(i).Width = 20;
                }


                var cellRange = worksheet.Cells[4, i, data.Count + 2, i]; // Từ dòng 4 đến dòng cuối cùng
               
                var cellBorder = cellRange.Style.Border;
                cellBorder.Left.Style = ExcelBorderStyle.Thin;
                cellBorder.Right.Style = ExcelBorderStyle.Thin;
                cellBorder.Top.Style = ExcelBorderStyle.Thin;
                cellBorder.Bottom.Style = ExcelBorderStyle.Thin;


                i++;

            }
            // Điền dữ liệu
            for (var j = 0; j < data.Count; j++)
            {
                var rowData = data[j];

                var rowIndex = j + 5; // Bắt đầu từ hàng thứ 5 để tránh ghi đè header

                worksheet.Cells[rowIndex, 1].Value = j + 1;

                int k = 2;
                foreach (var prop in props)
                {
                    // Lấy dữ liệu
                    var propValue = prop.GetValue(rowData, null);
                    if (propValue == null)
                    {
                        worksheet.Cells[rowIndex, k].Value = Resources.ResourceVN.Text_Unknown;
                    }
                    else
                    {
                        Type propType = prop.PropertyType;
                        if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                        {
                            worksheet.Cells[rowIndex, k].Value = ((DateTime)propValue).ToString("dd-MM-yyyy");
                        }
                        else
                        {
                            worksheet.Cells[rowIndex, k].Value = propValue;
                        }
                    }
                    k++;

                }
            }
            var titleCell = worksheet.Cells[1, 1, 2, props.Length];

           
            titleCell.Merge = true; // Gộp ô cho tiêu đề

            titleCell.Style.Font.Size = 14; // Kích thước chữ to

            titleCell.Style.Font.Bold = true; // Chữ in đậm

            titleCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Căn giữa

            worksheet.Cells[1, 1].Value = title.ToUpper();

            var stream = new MemoryStream(package.GetAsByteArray());
            return stream;
        }
        #endregion


    }

}
