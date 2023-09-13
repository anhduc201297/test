$(document).ready(function () {
    new EmployeePage();
});

class EmployeePage {

    constructor() {
        this.initEvents();

    }

    initEvents(){
        let self = this;
        //nút mở dialog thêm nhân viên
        $('#btnAddEmployee').click(function(){
            $('#formDialog').show();
            $('#employeeCode').focus();
        });

        $('#btnClose').click(function(){
            $('#formDialog').hide();
        });

        $('#btnEsc').click(function(){
            $('#formDialog').hide();
        });

        $('tbody tr').mouseover(function(){
            $('.table__functions').css('top',this.getBoundingClientRect().top - 209 + 'px');
        });
        
    }
}