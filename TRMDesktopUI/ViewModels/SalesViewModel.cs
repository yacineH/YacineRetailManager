using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUILibrary.API;
using TRMDesktopUILibrary.Model;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel :Screen
    {
        private IProductEndPoint _productEndPoint;

        public  SalesViewModel(IProductEndPoint productEndPoint)
        {
            _productEndPoint = productEndPoint;       
        }
          
        //c'est une astuce pour ne pas le mette dans le contructeur
        //car le constructeur ne peut pas etre async
        //et pour l'appeler on attend la fin de levenement onload
        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndPoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }


        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set 
            { 
                _products = value;
                NotifyOfPropertyChange(()=>Products);
            }
        }

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            { 
                _selectedProduct = value;
                NotifyOfPropertyChange(()=>SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BindingList<CartItemModel> _cart=new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set 
            { 
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity=1;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set 
            { 
                _itemQuantity = value;
                NotifyOfPropertyChange(()=>ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal 
        { 
            get
            {
                decimal subtotal = 0;

                foreach (var item in Cart)
                {
                   subtotal += item.Product.RetailPrice * item.QuantityInCart;
                }

                return subtotal.ToString("C");
            }
        }

        public string Tax
        {
            get
            {
                // TODO -replace with calculation
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                // TODO -replace with calculation
                return "$0.00";
            }
        }



        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                if(ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }
                return output;
            }
        }

        public void AddToCart()
        {
            CartItemModel existintItem = Cart.FirstOrDefault(x=>x.Product == SelectedProduct );
            if(existintItem!=null)
            {
                existintItem.QuantityInCart += ItemQuantity;

                ///hack it should be better for refreshing
                Cart.Remove(existintItem);
                Cart.Add(existintItem);
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);
            }
         
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(()=>SubTotal);

        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                //make sur there is something in the cart
                return output;
            }
        }

        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                //make sur there is something in the cart
                return output;
            }
        }

        public void CheckOut()
        {

        }
    }
}
