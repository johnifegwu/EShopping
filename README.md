
eShopping Microservices


![eShopping Architecture (1)](https://github.com/user-attachments/assets/fa4c2711-010e-4dac-97de-1b5cea3b2df3)

dotnet dev-certs https --clean
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx" -p "12345678"
dotnet dev-certs https --trust

Gateway-api [Micro-Service]
https://localhost:8081/swagger/index.html

User-api [Micro-Service]
https://localhost:9005/swagger/index.html

Ordering-api [Micro-Service]
https://localhost:9004/swagger/index.html

Catalog-api [Micro-Service]
https://localhost:9001/swagger/index.html

Basket-api [Micro-Service]
https://localhost:9002/swagger/index.html

Discount-api [Micro-Service]
https://localhost:8003/swagger/index.html