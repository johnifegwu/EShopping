
namespace Basket.Core.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = default!;
        public IList<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice
        {
            get
            {
                decimal result = 0;

                foreach (var item in Items)
                {
                    result += (item.Price * item.Quantity);
                }

                return result;
            }
        }

        public ShoppingCart() { }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        /// <summary>
        /// Adds the provided cart item to the list.
        /// </summary>
        /// <param name="item">Shopping Cart Item.</param>
        public void Add(ShoppingCartItem item)
        {
            if(Items.FirstOrDefault(x => x.ProductId == item.ProductId) == null)
            {
                Items.Add(item);
            }
            else
            {
                Update(item);

            }
        }

        /// <summary>
        /// Adds the provided shopping cart item to the users shopping cart if it does not exist, or
        /// Updates the users Shopping cart with the provided item if the quantity is greater than zero.
        /// or removes the given item from the shopping cart if the quantity is less than one.
        /// </summary>
        /// <param name="item">Shopping Cart Item.</param>
        public void Update(ShoppingCartItem item)
        {
            if(item.Quantity < 1)
            {
                Delete(item);
            }
            else
            {
                if (Items.FirstOrDefault(x => x.ProductId == item.ProductId) == null)
                {
                    Add(item);
                }
                else
                {
                    Items.FirstOrDefault(x => x.ProductId == item.ProductId).Quantity = item.Quantity;
                }
            }
        }

        /// <summary>
        /// Removes the provided cart item from the list.
        /// </summary>
        /// <param name="item">Shopping Cart Item.</param>
        public void Delete(ShoppingCartItem item)
        {
            var i = Items.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (i != null)
            {
                Items.Remove(i);
            }
        }

    }
}
