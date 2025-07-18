# 🎓 School Management System Backend

Sistem backend untuk manajemen sekolah yang mendukung pengelolaan data siswa, guru, kelas, dan pendaftaran (enrollment). Dibangun menggunakan arsitektur RESTful API.

## 🚀 Cara Menjalankan Secara Lokal

Pastikan Anda telah menginstal **Docker** dan **Docker Compose** di mesin Anda.

### 1. Clone repositori ini

```bash
git clone https://github.com/nama-user/nama-repo.git
cd nama-repo
```

### 2. Jalankan dengan Docker Compose

```bash
docker-compose up --build
```

Aplikasi akan berjalan di: [http://localhost:5283](http://localhost:5283)

---

## 📂 Deskripsi API & Contoh Request

### 🧑‍🎓 Students
- **GET** `/api/students?page=1&size=10&sortBy=fullName&sortDirection=desc`  
  Menampilkan daftar semua siswa.

- **POST** `/api/students`  
  Menambahkan siswa baru:
  ```json
  {
    "fullName": "Yusuf",
    "password": "SecurePass123!",
    "role": "student",
    "birthDate": "2005-04-15T00:00:00",
    "gender": "Laki-laki",
    "address": "Jl. Merdeka No. 10, Bandung"
  }
  ```
- **PUT** `/api/students/{id}`  
  Memperbarui informasi siswa.
  ```json
  {
    "studentId": 8,
    "fullName": "Yusuf Baru",
    "password": "SecurePass123!",
    "role": "student",
    "birthDate": "2005-04-15T00:00:00",
    "gender": "Laki-laki",
    "address": "Jl. Merdeka No. 10, Bandung"
  }
  ```
- **DELETE** `/api/students/{id}`  
  Menghapus siswa.

---

### 👨‍🏫 Teachers
- **GET** `/api/teachers?page=1&size=10&sortBy=fullName&sortDirection=desc`  
  Menampilkan daftar semua guru.

- **POST** `/api/teachers`  
  Menambahkan guru baru:
  ```json
  {
    "fullName": "Umar Bakri",
    "password": "SecurePass123!",
    "role": "student",
    "birthDate": "2005-04-15T00:00:00",
    "gender": "Laki-laki",
    "address": "Jl. Merdeka No. 10, Bandung"
  }
  ```

- **PUT** `/api/teachers/{id}`  
  Memperbarui informasi guru.
  ```json
  {
    "fullName": "Umar Ubah",
	"password": "SecurePass123!",
	"role": "teacher",
	"birthDate": "2005-04-15T00:00:00",
	"teacherId": 3,
	"gender": "Laki-laki",
	"address": "Jl. Merdeka No. 10, Bandung"
  }
  ```

- **DELETE** `/api/teachers/{id}`  
  Menghapus guru.

---

### 🏫 Classes
- **GET** `/api/classes?page=1&size=10&sortBy=name&sortDirection=desc`  
  Menampilkan semua kelas.

- **POST** `/api/classes`  
  Menambahkan kelas baru:
  ```json
  {
    "name": "Matematika 1",
  }
  ```

- **PUT** `/api/classes/{id}/assign-teacher/{teacherId}`  
  Menetapkan guru ke kelas:
  
- **PUT** `/api/classes/{id}/unassign-teacher`  
  Menghapus guru dari kelas.

---

### 📘 Enrollments
- **GET** `/api/enrollments?page=1&size=10&sortBy=studentName&sortDirection=desc`  
  Menampilkan semua pendaftaran dengan detail siswa dan kelas.

- **POST** `/api/enrollments`  
  Mendaftarkan siswa ke kelas (tanpa duplikasi):
  ```json
  {
    "studentId": 1,
    "classId": 2
  }
  ```

---

## 🛠️ Setup & Migrasi Database

1. Pastikan service database telah berjalan (otomatis dijalankan oleh Docker Compose).
2. Jika menggunakan Entity Framework Core (misalnya di .NET):

```bash
# Masuk ke dalam container aplikasi
docker-compose exec sms_service bash

# Jalankan migrasi database
dotnet ef database update
```

> Pastikan konfigurasi `ConnectionStrings` sudah mengarah ke environment variable, misalnya:
> 
> ```
> Host=sms_db;Port=5432;Database=sms_db;Username=postgres;Password=admin
> ```

---

## ✅ Fitur Utama

- CRUD untuk siswa dan guru
- Manajemen kelas dan pengajar
- Pendaftaran siswa ke kelas
- Validasi untuk mencegah pendaftaran duplikat

---

## 📬 Kontak

Jika Anda memiliki pertanyaan atau masukan, silakan hubungi kami melalui [email atau tautan kontak Anda di sini].