
__Khi một row được mounted chứng tỏ:__

**Search thay đổi:** `totalRecords` thay đổi, `data.length` ko rõ -	những row được check trc đó bị unmounted: emit lên (đã triển khai và chạy ổn với unit test)

**PageSize thay đổi:** `totalRecords` ko đổi, `data.length` thay đổi

**Page thay đổi:** `totalRecords` ko đổi, `data.length` ko đổi

__page đổi:__ unCheck header (vấn đề: các row đang checked bị unmounted cx emit lên) - O(n) với n là số lg row đang đc check - Đã cài đặt, chấp nhận issue

__Phương án 1__ (đã loại bỏ)
*Nếu đang check header:* 

__search thay đổi:__ 

-	những row mới được thêm vào: 
	```
	Tìm 1 row gần nhất chưa được check: O(pageSize) -> unCheck header (loại bỏ)
	Row tự emit lên khi được mounted: O(n) - với n là số row được thêm vào (đã triển khai và bị loại bỏ)
	```
*Nếu đang unCheck header:*

__search thay đổi:__ 
+ totalRecords giảm: những row được check trc đó vẫn còn trong data -> kiểm tra checkedAmount với data.length ()
+ totalRecords tăng: unCheck header



__Phương án 2: updated__ (đã triển khai)
*updated__ của table chạy khi:*
- pageSize, data, page
- Mỗi khi update table: check amountItem với data.length (đã triển khai và chạy ổn với unit test)
