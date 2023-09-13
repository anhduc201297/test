const commonJS = {
    formatDate(date){
        try{
            date = new Date(date);
            let dateString = date.getDate();
            dateString = dateString < 10 ? `0${dateString}` : dateString; 
            let monthValue = date.getMonth() + 1;
            monthValue = monthValue < 10 ? `0${monthValue}` : monthValue; 
            let yearValue = date.getFullYear();
            return `${dateString}/${monthValue}/${yearValue}`;
        }catch{
            return "";
        }
    }
}