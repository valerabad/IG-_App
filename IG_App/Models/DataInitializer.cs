using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BD_App.Models
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseAlways<DataContext>
    {
        public string SeedCustomerQuery()
        {
            return @"CREATE PROCEDURE [dbo].[SeedCustomers]
                    @count int,
                    @name nvarchar(50),
                    @address nvarchar(50),
                    @categorry tinyint
                    AS
                    declare @i int = 0
                    while @i < @count
                    begin
                    insert into Customers  (Name, Address, Categorry) values(CONCAT(@name + ' ', @i), CONCAT(@address + ' ', @i), (cast(0 + (RAND(checksum(newid())) * 4) as int)))
                       set @i = @i + 1;
                            end
                        RETURN 0";
        }

        public string SeedOrdersQuery()
        {
            return @"CREATE PROCEDURE [dbo].[SeedOrders]
            AS
            declare @countCustomers int
            declare @randomCountOrders5_50 int
            set @countCustomers = (select COUNT(Customers.ID) from Customers);
                        declare some_cursor cursor
            --sql запрос любой сложности, формирующий набор данных для курсора
             for select Customers.ID from Customers

            --открываем курсор
            open some_cursor
            --обьявляем переменные
                declare  @counter int
                declare  @int_var int, @string_var varchar(100)

                set @counter = 0
                -- выборка первой  строки

                fetch next from some_cursor INTO  @int_var
                -- цикл с логикой и выборкой всех последующих строк после первой

                while @@FETCH_STATUS = 0
                begin
                    declare @countOrders int
                    set @countOrders = 0
					--генерируем от 5 до 50 заказов для каждого клиента
                    set @randomCountOrders5_50 = (cast(5 + (RAND(checksum(newid())) * 46) as int))

                    while @countOrders < @randomCountOrders5_50
                    begin
                       insert into Orders (Date, CustomerID) values(GETDATE(), @int_var)
                       set @countOrders = @countOrders + 1
                    end
                        -- - логика внутри цикла
                        set @counter = @counter + 1
                         if @counter >= @countCustomers break  --возможный код для проверки работы, прерываем после пятой итерации
                       -- отладочный select, на большом количестве строк выборка данных в  sql server management studio может привести к ошибке переполнения памяти
                        SELECT @int_var
                        fetch next from some_cursor INTO  @int_var
                end
                select @counter as final_count
                close some_cursor
                --deallocate some_cursor";
        }

        public string SeedProductsQuery()
        {
            return @"CREATE PROCEDURE [dbo].[SeedProducts]
	        @count int,
	        @title nvarchar(50)
            AS
            declare @i int = 0
	        while @i < @count
	        begin
		        insert into Products (Title) values (CONCAT(@title+' ',@i))
		           set @i = @i + 1;
	        end
	
            RETURN 0";
        }

        public string SeedOrder_PtoductQuery()
        {
            return @"CREATE PROCEDURE [dbo].[SeedOrder_Product] 
            AS
                delete from Order_Product
	            DECLARE @ID INT
		            /*Объявляем курсор*/
		            DECLARE @CURSOR CURSOR
		            /*Заполняем курсор*/
		            SET @CURSOR  = CURSOR SCROLL
		            FOR
		            SELECT  ID 
		              FROM  Orders
		            /*Открываем курсор*/
		            OPEN @CURSOR
		            /*Выбираем первую строку*/
		            FETCH NEXT FROM @CURSOR INTO @ID
		            /*Выполняем в цикле перебор строк*/
		            WHILE @@FETCH_STATUS = 0
		            BEGIN
		            declare @i int
		            declare @random_value1_100 int
		            set @i = 0
		            --в каждом заказе 1-100 товаров
		            set @random_value1_100 = (cast(1 + (RAND(checksum(newid())) * 101) as int))
			            while @i<@random_value1_100
			            begin
                          declare @pr int = 3--(cast(20 + (RAND(checksum(newid())) * 60) as int))
						  declare @ct int = 2--(cast(1 + (RAND(checksum(newid())) * 20) as int))
						  declare @total int = @pr*@ct
			              insert into Order_Product (Orders_ID, Price, Count, Total) values (@ID, @pr, @ct, @total )
			              --insert into Order_Product (Orders_ID) values (@ID)
			              set @i = @i +1
			            end
		            /*Выбираем следующую строку*/
		            FETCH NEXT FROM @CURSOR INTO @ID
		            END
        
                CLOSE @CURSOR
                exec AddingProductsID";
                        }

        public string AddingProductsIDQuery()
        {
            return @"CREATE PROCEDURE [dbo].[AddingProductsID] 

            AS
  
	            DECLARE @ID INT
		            /*Объявляем курсор*/
		            DECLARE @CURSOR CURSOR
		            /*Заполняем курсор*/
		            SET @CURSOR  = CURSOR SCROLL
		            FOR
		            SELECT  Order_Product.ID
		              FROM  Order_Product
		            /*Открываем курсор*/
		            OPEN @CURSOR
		            /*Выбираем первую строку*/
		            FETCH NEXT FROM @CURSOR INTO @ID
		            /*Выполняем в цикле перебор строк*/
		            WHILE @@FETCH_STATUS = 0
		            BEGIN
			             declare @randomProductId int
			             --выбираем в заказ 1 из 5000 товаров
			             set @randomProductId = (cast(1 + (RAND(checksum(newid())) * 4996) as int))
			             update Order_Product set Products_ID = @randomProductId where Order_Product.ID = @ID
		            /*Выбираем следующую строку*/
		            FETCH NEXT FROM @CURSOR INTO @ID
		            END
	
            CLOSE @CURSOR";
                    }

        protected override void Seed(DataContext db)
        {
            //вызов происходит в случае изменения модели. Тогда пересоздаётся база
            db.Database.ExecuteSqlCommand(SeedCustomerQuery());
            db.Database.ExecuteSqlCommand(SeedOrdersQuery());
            db.Database.ExecuteSqlCommand(SeedProductsQuery());
            // запоняем таблицу Клиенты, 100 клиентов
            SqlParameter param1 = new SqlParameter("@count", 100);
            SqlParameter param2 = new SqlParameter("@name", "Client");
            SqlParameter param3 = new SqlParameter("@address", "Address");
            SqlParameter param4 = new SqlParameter("@categorry", 1);
            db.Database.ExecuteSqlCommand("[dbo].[SeedCustomers] @count, @name, @address, @categorry", param1, param2, param3, param4);

            // заполняем таблицу Заказы
            db.Database.ExecuteSqlCommand("[dbo].[SeedOrders]");

            //заполняем таблицу Продукты, 5000 штук
            param1 = new SqlParameter("@count", 5000);
            param2 = new SqlParameter("@title", "Product");
            db.Database.ExecuteSqlCommand("[dbo].[SeedProducts] @count, @title", param1, param2);

            db.Database.ExecuteSqlCommand(AddingProductsIDQuery());
            db.Database.ExecuteSqlCommand(SeedOrder_PtoductQuery());
            db.Database.ExecuteSqlCommand("[dbo].[SeedOrder_Product]");
            //// запоняем таблицу Клиенты, 100 клиентов
            //SqlParameter param1 = new SqlParameter("@count", 100);
            //SqlParameter param2 = new SqlParameter("@name", "Client");
            //SqlParameter param3 = new SqlParameter("@address", "Address");
            //SqlParameter param4 = new SqlParameter("@categorry", 1);
            //db.Database.ExecuteSqlCommand("[dbo].[SeedCustomers] @count, @name, @address, @categorry", param1, param2, param3, param4);

            //// заполняем таблицу Заказы
            //db.Database.ExecuteSqlCommand("[dbo].[SeedOrders]");

            ////заполняем таблицу Продукты, 5000 штук
            //param1 = new SqlParameter("@count", 5000);
            //param2 = new SqlParameter("@title", "Product");
            //db.Database.ExecuteSqlCommand("[dbo].[SeedProducts] @count, @title", param1, param2);

            //Customer client = new Customer { Name = "new2", Address = "ewf", Categorry = 1 }; //Categories.middle
            //Customer client2 = new Customer { Name = "yes2", Address = "fhgh", Categorry =  2}; //
            ////Customers client3 = new Customers { Name = "dhfdh", Address = "dhfdh", Categorry = "dhdh" };
            ////Customers client4 = new Customers { Name = "32352352", Address = "dhh", Categorry = "df" };

            //db.Customer.Add(client);
            //db.Customer.Add(client2);
            //db.SaveChanges();

        }

        public void SeedByStoredProc()
        {
           
        }
    }
}