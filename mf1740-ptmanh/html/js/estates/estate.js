window.onload = function(){
    new EstatePage();  
}
class EstatePage{
    
    constructor(){
        this.loadData();
        //Khai báo các element được chọn
        const btnUpgrades = document.querySelectorAll('.icon--edit');
        const closeBtns = document.querySelectorAll('.modal__header--close');
        const cancelBtns = document.querySelectorAll('.cancel');
        // Gán sự kiện cho btn xem chi tiết
        for(const btnUpgrade of btnUpgrades){
            btnUpgrade.addEventListener('click', this.showForm);
        }
        // Gán sự kiện cho btn đóng form
        for (const closeBtn of closeBtns) {
            closeBtn.addEventListener('click', this.closeForm);
        }
        // Gán sự kiện cho nút hủy
        for (const cancelBtn of cancelBtns) {
            cancelBtn.addEventListener('click', this.closeForm);
        }

    }
    loadData(){
        
    }
    // Hàm thực hiện chức năng hiển thị form
    // Author: ptmanh
    showForm(){
        document.querySelector('.modal').classList.remove('hidden');
        document.querySelector('.modal input').focus();
    }
    closeForm(){
        document.querySelector('.modal').classList.add('hidden');
    }
    
}