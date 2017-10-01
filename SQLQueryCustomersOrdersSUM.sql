--select  Order_Product.Orders_ID, Sum(Order_Product.Total) as 'Total orders'
--from Order_Product
--where Orders_ID = 6
--group by Order_Product.Orders_ID
--order by Order_Product.Orders_ID
--select distinct Orders.CustomerID, Customer.Name from Orders, Customers

select CustomerID, Name, Address, SUM(po.total) as 'sum'
from Orders AS o join Order_Product as po 
	on o.ID = po.Orders_ID
	join Customers AS c
	on c.ID = o.CustomerID
	group by o.CustomerID, Name, Address
	order by o.CustomerID