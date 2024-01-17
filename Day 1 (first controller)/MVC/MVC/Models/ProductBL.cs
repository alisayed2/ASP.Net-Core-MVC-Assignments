namespace MVC.Models
{
    public class ProductBL
    {
        public static List<Product> Products = new List<Product>();
        static ProductBL()
        {
            Products.Add(new Product() { Id = 1, Name = "Laptop",Image = "p1.jpg", Price=20000});
            Products.Add(new Product() { Id = 2, Name = "Smart Watch",Image = "p2.jpg", Price = 4500 });
            Products.Add(new Product() { Id = 3, Name = "Head Phone",Image = "p3.jpg", Price = 1500 });
        }

        public static List<Product> getProducts()
        {
            return Products;
        }

        public static Product getProductById(int id)
        {
            return Products.FirstOrDefault(p=>p.Id==id);
        }
    }
}
