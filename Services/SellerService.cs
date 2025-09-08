using AutoMapper;
using ProductsControl.Helpers;
using ProductsControl.DTO;
using ProductsControl.Infrastructure.IProviders;
using ProductsControl.Interfaces;
using ProductsControl.Models;

namespace ProductsControl.Services
{
    public class SellerService : ISellerService
    {
        private readonly IProductDbProvider _productDbProvider;
        private readonly IOrderDbProvider _orderDbProvider;
        private readonly IMapper _mapper;

        public SellerService(IProductDbProvider productDbProvider, IOrderDbProvider orderDbProvider, IMapper mapper)
        {
            _productDbProvider = productDbProvider;
            _orderDbProvider = orderDbProvider;
            _mapper = mapper;
        }

        public async Task<bool> AddProductAsync(AddProductDto addProductDto, int sellerId)
        {
            if (await _productDbProvider.GetProductByNameAsync(addProductDto.Name) != null)
            {
                throw new Exception("That product already exists.");
            }

            Product product = _mapper.Map<Product>(addProductDto);

            if (addProductDto.ImageFile != null)
            {
                product.Image = await ImageConverter.ConvertToByteArray(addProductDto.ImageFile);
            }
            else
            {
                product.Image = new byte[0];
            }
            product.SellerId = sellerId;

            return await _productDbProvider.AddProduct(product);
        }

        public async Task<List<GetProductDto>> GetProductsAsync(int sellerId)
        {
            List<Product>? products = await _productDbProvider.GetAllProductsAsync(sellerId);

            if (products == null)
            {
                throw new Exception("You are don't have any products.");
            }

            return _mapper.Map<List<GetProductDto>>(products);
        }

        public async Task<bool> RemoveProductAsync(int id, int sellerId)
        {
            Product? product = await _productDbProvider.GetProductByIdAsync(id);

            if (product == null)
            {
                throw new Exception("That product doesn't exist.");
            }

            if (sellerId != product.SellerId)
            {
                throw new Exception("You can remove your products only.");
            }

            return await _productDbProvider.RemoveProduct(product);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto, int sellerId)
        {
            Product? product = await _productDbProvider.GetProductByIdAsync(updateProductDto.Id);

            if (product == null)
            {
                throw new Exception("Product doesn't exist.");
            }

            if (sellerId != product.SellerId)
            {
                throw new Exception("Not your product.");
            }

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.Amount = updateProductDto.Amount;
            if (updateProductDto.ImageFile != null && updateProductDto.ImageFile.Length != 0)
            {
                product.Image = await ImageConverter.ConvertToByteArray(updateProductDto.ImageFile);
            }
            else if (updateProductDto.Image != null && updateProductDto.Image.Length != 0)
            {
                product.Image = updateProductDto.Image;
            }
            else
            {
                product.Image = new byte[0];
            }

            await _productDbProvider.UpdateProduct(product);
        }

        public async Task<List<GetOrderDto>> GetNewOrdersAsync(int sellerId)
        {
            List<Order> orders = await _orderDbProvider.GetNewOrdersForSellerAsync(sellerId);

            return _mapper.Map<List<GetOrderDto>>(orders);
        }

        public async Task<List<GetOrderDto>> GetOldOrdersAsync(int sellerId)
        {
            List<Order> orders = await _orderDbProvider.GetOldOrdersForSellerAsync(sellerId);

            return _mapper.Map<List<GetOrderDto>>(orders);
        }

        public async Task<GetProductDto> GetProductAsync(int id, int sellerId)
        {
            Product? product = await _productDbProvider.GetProductByIdAsync(id);

            if (product == null)
            {
                throw new Exception("That product doesn't exist.");
            }
            if (product.SellerId != sellerId)
            {
                throw new Exception("You can get your products only.");
            }

            return _mapper.Map<GetProductDto>(product);
        }
    }
}
