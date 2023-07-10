using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyBud.CartApi.Interfaces;
using MyBud.CartApi.Models.Core;
using MyBud.CartApi.Models.Request;
using System.Net;
using System.Security.Claims;

namespace MyBud.CartApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _CartRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly Guid _userId;

        public CartController(ICartRepository CartRepository,
        IHttpContextAccessor httpContext)
        {
            _CartRepository = CartRepository;
            _httpContext = httpContext;
            _userId = Guid.Parse(_httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        /// <summary>
        /// Get list of all Cart
        /// </summary>
        /// <returns>List of all Carts</returns>
        [ProducesResponseType(typeof(IEnumerable<CartEntity>), (int)HttpStatusCode.OK)]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var Carts = await _CartRepository.GetCartItems(_userId);

            return Ok(Carts);
        }

        /// <summary>
        /// Get details of a specific Cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Details of a specific Cart</returns>
        [ProducesResponseType(typeof(CartEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemById(int id)
        {
            var product = await _CartRepository.GetCartItemById(id);

            return product != null
                ? Ok(product)
                : NotFound();
        }

        ///// <summary>
        ///// Search Carts
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns>List of matching Cart</returns>
        //[ProducesResponseType(typeof(IEnumerable<CartEntity>), (int)HttpStatusCode.OK)]
        //[HttpGet("Search")]
        //public async Task<IActionResult> SearchCarts(string value)
        //{
        //    var Cart = await _CartRepository.SearchCarts(value);

        //    return Cart != null
        //        ? Ok(Cart)
        //        : NotFound();
        //}

        /// <summary>
        /// Create Cart
        /// </summary>
        /// <param name="Cart"></param>
        /// <returns>Result of Cart creation</returns>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(IDictionary<string, string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCart(CreateCartItem createCart)
        {
            //TODO: validate model
            var cart = new CartEntity
            {
                ProductId=createCart.ProductId,
                UserId = _userId,
                DateAdded = DateTime.UtcNow,
                Quantity = createCart.Quantity
            };

            var createdCart = await _CartRepository.CreateOrUpdateCart(cart);

            return createdCart != null
                ? Created(new Uri(Request.GetEncodedUrl()), createdCart)
                : BadRequest();
        }

        ///// <summary>
        ///// Update specific Cart
        ///// </summary>
        ///// <param name="Cart"></param>
        ///// <returns>Updated Cart</returns>
        //[HttpPut]
        //[Authorize]
        //[ProducesResponseType(typeof(CartEntity), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> UpdateCart(CartEntity Cart)
        //{
        //    //ToDo: Validate input model
        //    var isExistingCart = await _CartRepository.GetCartItemById(Cart.CartId) != null;

        //    if (isExistingCart)
        //    {
        //        var updatedCart = await _CartRepository.UpdateCart(Cart);

        //        return Ok(updatedCart);
        //    }

        //    return NotFound(Cart);
        //}

        ///// <summary>
        ///// Delete specific Cart
        ///// </summary>
        ///// <param name="CartId"></param>
        ///// <returns>Result of Cart deletion</returns>
        //[ProducesResponseType(typeof(CartEntity), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[HttpDelete("{id}")]
        //[Authorize]
        //public async Task<IActionResult> DeleteCart(int CartId)
        //{
        //    var Cart = await _CartRepository.GetCartItemById(CartId);
        //    if (Cart == null)
        //    {
        //        return NotFound();
        //    }

        //    var isDeleted = await _CartRepository.DeleteCart(Cart);

        //    return isDeleted
        //        ? Ok()
        //        : NotFound();
        //}
    }
}