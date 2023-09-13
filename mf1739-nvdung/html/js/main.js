// import các file

// import js\table
import * as tableAddTitle from "./table/add-title.js";
import * as tableRowSelected from "./table/row-selected.js";
import * as tableRowChecked from "./table/row-checked.js";
// import js\combobox
import * as cboHover from "./combobox/hover.js";
import * as cboActive from "./combobox/active.js";
import * as cboError from "./combobox/error.js";
import * as cboShowHideDropdownlist from "./combobox/show-hide-dropdownlist.js";
import * as cboItemSelected from "./combobox/item-selected.js";
// import js\popup
import * as openClosePopup from "./popup/open-close-popup.js";

document.addEventListener("DOMContentLoaded", function () {
  // js\table
  tableAddTitle.setTitleAllCells();
  tableRowSelected.rowSelected();
  tableRowChecked.rowChecked();

  // js\combobox cho phân trang
  cboHover.cboHover("record-in-page");
  cboActive.cboActive("record-in-page");
  cboError.cboError("record-in-page");
  cboShowHideDropdownlist.cboShowHideDropdownlist("record-in-page");
  cboItemSelected.itemSelected("record-in-page");

  // js\popup
  openClosePopup.openPopup();
});
