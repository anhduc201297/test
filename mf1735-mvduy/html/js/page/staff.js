window.onload = () => {
    new Staff();
}

class Staff {
    constructor() {
        this.loadData();
        document.getElementById('btnAdd').addEventListener('click', this.btnAddOnClick);
        document.querySelector('.header-form .close-icon').addEventListener('click', this.btnCloseOnClick);
        document.querySelector('.footer-form .btn-normal').addEventListener('click', this.btnCloseOnClick);

        var onClickDelete = document.querySelectorAll('#tdStaff.select-content a:nth-child(2)');
        for (const click of onClickDelete) {
            click.addEventListener('click', this.onClickDeleteDialog);
        }

        document.querySelector('.delete-notify-dialog .close-icon').addEventListener('click', this.closeClickDeleteDialog);
        document.querySelector('.delete-notify-dialog .btn-normal').addEventListener('click', this.closeClickDeleteDialog);
    }
    /* 
    * Click hiển thị form
    * author: mf1735-mvduy (15/08/2023)
    */
    btnAddOnClick() {
        document.querySelector('.d-form-popup').style.visibility = 'visible';
        document.querySelector('.d-detail-form .d-input').focus();
    }

    /* 
    * Click đóng form
    * author: mf1735-mvduy (15/08/2023)
    */
    btnCloseOnClick() {
        document.querySelector('.d-form-popup').style.visibility = 'hidden';
    }

    /*
     * Click hiển thị delete dialog  
     * Author: mf1735-mvduy (16/08/2023)
     */

    onClickDeleteDialog() {
        document.querySelector('.d-delete-notify-popup').style.visibility = 'visible';

    }

    /*
    * Click đóng delete dialog  
    * Author: mf1735-mvduy (16/08/2023)
    */
    closeClickDeleteDialog() {
        document.querySelector('.d-delete-notify-popup').style.visibility = 'hidden';
    }

    /*
     * Load dữ liệu
     * Author: mf1735-mvduy (16/08/2023)
     */

    loadData() {
        try {
            fetch('https://cukcuk.manhnv.net/api/v1/customers')
                .then(res => res.json())
                .then(data => {
                    for (const staff of data) {
                        let trElement = document.createElement("tr");
                        trElement.innerHTML = `
                    <td class="text-center" style="width: 20px;"><input type="checkbox"></td>
                    <td class="text-left" style="width: 70px;">${staff.CustomerCode || ""}</td>
                    <td class="text-left">${staff.FullName || ""}</td>
                    <td class="text-left" style="width: 40px;">${commonJS.formatGender(staff.Gender) || ""}</td>
                    <td class="text-center">${commonJS.formatDate(staff.DateOfBirth) || ""}</td>
                    <td class="text-left">123456789</td>
                    <td class="text-left">Fresher</td>
                    <td class="text-left">${staff.CompanyName || ""} </td>
                    <td class="text-left">${staff.DebitAmount || ""}</td>
                    <td class="text-left"></td>
                    <td class="text-left"></td>
    
                    <td class="text-center" style="width: 50px;">Sửa
                        <div class="down-select">
                            <div class="down-select-button">
                                <div class=" icon icon-triangle-down"></div>
                            </div>
                            <div class="select-content">
                                <a href="#">Nhân bản</a>
                                <a href="#" onclick="(
                                    () => { document.querySelector('.d-delete-notify-popup').style.visibility = 'visible'; 
                                })()">Xóa</a>
                                <a href="#">Ngừng sử dụng</a>
                            </div>
                        </div>
                    </td>`
                        document.querySelector('#tdStaff').append(trElement);
                    }
                })
        } catch (error) {
            console.log(error)
        }

    }
}

