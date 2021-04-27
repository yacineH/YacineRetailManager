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

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set 
            { 
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set 
            { 
                _itemQuantity = value;
                NotifyOfPropertyChange(()=>ItemQuantity);
            }
        }

        public string SubTotal 
        { 
            get
            {
                // TODO -replace with calculation
                return "$0.00";
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


                return output;
            }
        }

        public void AddToCart()
        {

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
