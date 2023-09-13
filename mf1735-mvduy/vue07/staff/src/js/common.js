const commonJS = {
    formatDate(date){
        try {
            date = new Date(date);
            let dateString = date.getDate();
            dateString = dateString < 10 ? `0${dateString}` :dateString;
            let monthValue = date.getMonth() + 1;
            monthValue = monthValue < 10 ? `0${monthValue}` :monthValue;
            let yearValue = date.getFullYear();
            return `${dateString}/${monthValue}/${yearValue}`
        } catch(error){
            return "";
        }
    },

    formatGender(num){
        try {
            let gender;
            if (num == 1)  gender = 'Nam'
            else if (num == 2) gender = 'Nữ'
            else gender = ''
            return gender;
        } catch (error) {
            console.log(error)
        }
    }
    
}

export default commonJS;