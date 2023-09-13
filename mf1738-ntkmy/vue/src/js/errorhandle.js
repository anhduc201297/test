/**
    * Hàm xử lý error
    * CreateBy : NTKMY (30/08/2023)
*/
export function handleError(error) {
    const code = error.response.status;
    switch (code) {
      case 500:
      case 400:
      case 401:
      case 403:
      case 404:
        this.showErrorToast();
        break;
      default:
        console.error('Error:', error);
        this.showErrorToast();
        break;
    }
  }