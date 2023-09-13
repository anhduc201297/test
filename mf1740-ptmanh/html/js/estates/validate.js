const form = document.getElementById('detail-form');
const estate_id = document.getElementById('estate__id');
const estate_name = document.getElementById('estate__name');
const department_id = document.getElementById('department__id');
const category_id = document.getElementById('estate__category-id');
const estate_quantity = document.getElementById('estate__quantity');
const original_price = document.getElementById('original-price');
const year_used = document.getElementById('year-used');
const wear_rate = document.getElementById('wear-rate');
const depreciated_value = document.getElementById('depreciated-value');
const purchase_date = document.getElementById('purchase-date');
const using_date = document.getElementById('using-date');

const tracking_year = document.getElementById('tracking-year');

form.addEventListener('submit', e => {
    e.preventDefault();
    validateInputs();
});
const setError = (element, message) => {
    const inputControl = element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');

    errorDisplay.innerText = message;
    inputControl.classList.add('error');
    inputControl.classList.remove('success')
}

const setSuccess = element => {
    const inputControl = element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');
    errorDisplay.innerText = '';
    inputControl.classList.add('success');
    inputControl.classList.remove('error');
};
function checkExist(value, element){
    if(value === ''){
        setError(element, 'Không được để trống');
    }else{
        setSuccess(element);
    }
}
const validateInputs = () =>{
    const estateID = estate_id.value.trim();
    const estateName = estate_name.value.trim();
    const departmentID = department_id.value.trim();
    const categoryID = category_id.value.trim();
    const estateQuantity = estate_quantity.value.trim();
    const originalPrice = original_price.value.trim();
    const yearUsed = year_used.value.trim();
    const wearRate = wear_rate.value.trim();
    const depreciatedValue = depreciated_value.value.trim();
    const purchaseDate = purchase_date.value.trim();
    const usingDate = using_date.value.trim();
    
    checkExist(estateID, estate_id);
    checkExist(estateName, estate_name); 
    checkExist(departmentID, department_id); 
    checkExist(categoryID, category_id); 
    checkExist(estateQuantity, estate_quantity); 
    checkExist(originalPrice, original_price); 
    checkExist(yearUsed, year_used); 
    checkExist(wearRate, wear_rate); 
    checkExist(depreciatedValue, depreciated_value); 
    checkExist(purchaseDate, purchase_date); 
    checkExist(usingDate, using_date); 
       
}