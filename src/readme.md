Lưu ý : 
	- 1 : Thuộc tính của model phải khớp với các trường trong DB (Lý do xem tại class Convert ở Layer Bussiness)
	- 2 : Băt buộc phải có phương thức khởi tạo không tham số (Lý do xem tại class Convert ở Layer Bussiness)

Layer src :
	* Interface :
		- ICRUD<T> :
			+ T là 1 class trong model 
			+ Gồm các phương thức chính để tương tác với dữ liệu 
				. FindAll() : Lấy ra tất cả các bản ghi
				. FindOne(Ikey key) : pramas là khóa chính của Model , Kết quả trả về là 1 obj T
				. Update(T obj) : pramas là 1 Model , Thực hiện sửa thông tin của 1 obj Model 
				. Create(T obj) : pramas là 1 Model , Thực hiện thêm 1 obj Model
				. Delete(Ikey key) :  pramas là khóa chính của Model , Thực hiện xóa 1 obj Model dựa vào khóa chính
			+ duoc imlement boi các controller
	* Controller :
		- Các controller mới sau khi được tạo cần imlement ICRUD
		- Các controller đảm nhiệm công việc thông báo kết quả của 1 request
Layer Model :
	* Interface :
		- IKey : 
		- IKey[Tên Model] : 
			+ Kế thừa IKey
			+ Chứa các trường tạo nên khóa chính của Model
			+ Các Model cần imlement vì bất cứ model nào cũng có khóa chính
	* Các Model :
		- ResponseResult<T> :
			+ T là các model
			+ Properties StatusCode : kiểu dữ liệu int , mã trạng thái của 1 request (Ví dụ 200 là request thành công , 500 là thất bại)
			+ Properties Message : Kiểu dữ liệu string , dòng text thông báo về kết quả của request
			+ Properties Data : Dữ liệu trả về của request
		- Các Model khác : 
			+ Là các bảng trong DB
			+ Thuộc tính của model phải khớp với các trường trong DB
			+ Băt buộc phải có phương thức khởi tạo không tham số
Layer Bussiness :
	* Layer này có nhiệm vụ kiểm tra dữ liệu đầu vào và chuyển đổi các kiểu dữ liệu khác thành model để Layer Src sử dụng
	* Middlware :
		- Convert<T> : Chứa các phương thức để chuyển đổi dữ liệu datatable , datarow thành các model
	* Interface : 
		- IValidate : 
			+ Chứa các phương thức để validate 1 model 
			+ Khi 1 model mới được tạo cần tạo class BUS của đối tượng này rồi imlement IValidate để thực hiện công việc validate dữ liệu
	* Class ControlManager (Design Partent Factory Method) :
		- Class này có nhiệm vụ tạo các instance của các đối tượng lớp DAL và BUS dựa vào tên Model
	* Class Bussiness<T> :
		- Class này có nhiệm vụ giao tiếp trực tiếp với Layer Src
		- Với T là 1 Model class này sẽ gọi ControlManager để tạo các instance tương ứng với T rồi sau đó thực hiện các thao tác với dữ liệu
Layer DataAccess :
	* Interface :
		- ICRUD :
	* Class Dataprovider :
		- Chứa các phương thức thực hiện câu query trong DB
	* Các class còn lại :


Quy ước :
	* Statuscode của response :
		- 1x : Lỗi xác thực ( Authentication Errors )
			+ 11 : Lỗi Không có quyền truy cập (Access Denied)
			+ 12 : Lỗi Token Xác thực hết hạn (Expired Token)
			+ 13 : Lỗi Token không hợp lệ (Invalid Token)
			+ 14 : Lỗi Mật khẩu quá yếu (Weak Password)
			+ 15 : Lỗi Sai Mật khẩu (Invalid Credentials)
			+ 16 : Lỗi chưa đăng nhập ( chưa có token )
		- 2x : Lỗi liên quan đến DB
			+ 21 : Lỗi Trùng lặp dữ liệu (Duplicate Data)
			+ 22 : Lỗi Ràng buộc Dữ liệu (Data Constraint Violation)
			+ 23 : Lỗi Kết nối Cơ sở dữ liệu (Database Connection Error)
		- 3x : Lỗi do input người dùng 
			+ 31 : Lỗi Định dạng không đúng (Format Error):Dữ liệu nhập không tuân theo định dạng yêu cầu, ví dụ: định dạng ngày tháng không đúng.
			+ 32 : Lỗi Quá dài hoặc Quá ngắn (Length Error)
			+ 33 : Lỗi Xác nhận mật khẩu không khớp (Password Confirmation Error)
		- 4x : Lỗi hacker
			+ 41 : Sai khóa xác thực token