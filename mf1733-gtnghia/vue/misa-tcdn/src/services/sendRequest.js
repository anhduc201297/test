async function sendRequest(URL, method = "get", data) {
  // this.$loading = true;
  try {
    const response = await this.$axios.request({
      url: URL,
      method: method,
      data: data,
      headers: {
        "Content-Type": "application/json-patch+json",
      },
    });
    return response.data;
  } catch (error) {
    console.error(error);
    const handleError = this.$MISAHelper.handleErrorResponse(error)
    console.log(handleError);
    return error
  } finally {
    // this.$loading = false;
  }
}

export default sendRequest;
