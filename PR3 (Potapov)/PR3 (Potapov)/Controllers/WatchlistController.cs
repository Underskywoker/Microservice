using Microsoft.AspNetCore.Mvc;
using PR3__Potapov_.Item;

[ApiController]
[Route("api/[controller]")]
public class WatchlistController : ControllerBase
{
    private static List<WatchlistItem> watchlist = new List<WatchlistItem>();

    /// <summary>
    /// Получить все элементы из списка отслеживания.
    /// </summary>
    /// <returns>Список всех элементов.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(watchlist);
    }

    /// <summary>
    /// Добавить элемент в список отслеживания.
    /// </summary>
    /// <param name="item">Данные элемента для добавления (UserId, ProductId, ContactInfo).</param>
    /// <returns>Добавленный элемент с присвоенным ID.</returns>
    [HttpPost]
    public IActionResult AddToWatchlist([FromBody] WatchlistItem item)
    {
        item.Id = watchlist.Count + 1; // Простая автоинкрементация
        watchlist.Add(item);
        return CreatedAtAction(nameof(GetAll), new { id = item.Id }, item);
    }

    /// <summary>
    /// Удалить элемент из списка отслеживания по его ID.
    /// </summary>
    /// <param name="id">ID элемента, который нужно удалить.</param>
    /// <returns>Статус: 204 (успешное удаление) или 404 (если элемент не найден).</returns>
    [HttpDelete("{id}")]
    public IActionResult RemoveFromWatchlist(int id)
    {
        var item = watchlist.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return NotFound("Товар не найден");
        }
        watchlist.Remove(item);
        return NoContent();
    }

    /// <summary>
    /// Отправить уведомления пользователям о продуктах в их списке отслеживания.
    /// </summary>
    /// <returns>Сообщение об успешной отправке уведомлений.</returns>
    [HttpPost("notify")]
    public IActionResult NotifyUsers()
    {
        foreach (var item in watchlist)
        {
            Console.WriteLine($"Notify {item.ContactInfo} about Product {item.ProductId}");
        }
        return Ok("Уведомления отправлены.");
    }
}
