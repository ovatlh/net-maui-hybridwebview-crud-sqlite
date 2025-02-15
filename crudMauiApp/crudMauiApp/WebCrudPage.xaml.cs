using crudMauiApp.Data.Entities;
using System.Diagnostics;
using System.Text.Json;

namespace crudMauiApp;

public partial class WebCrudPage : ContentPage
{
    public WebCrudPage()
    {
        InitializeComponent();
        HWVControl.SetInvokeJavaScriptTarget(this);
    }

    public async Task DeleteItemByIdAsync(int id)
    {
        var itemList = await App.GlobalApp.RefItemRepository().ReadAsync(filter: x => x.Id == id);
        var itemDB = itemList.FirstOrDefault();
        if (itemDB != null)
        {
            await App.GlobalApp.RefItemRepository().DeleteAsync(itemDB);
        }
    }

    public async Task UpdateItemAsync(object itemJson)
    {
        var item = JsonSerializer.Deserialize<Item>(itemJson.ToString());
        if (item != null)
        {
            var itemListDB = await App.GlobalApp.RefItemRepository().ReadAsync(filter: x => x.Id == item.Id);
            var itemDB = itemListDB.FirstOrDefault();

            if (itemDB != null)
            {
                itemDB.Name = item.Name;
                await App.GlobalApp.RefItemRepository().UpdateAsync(itemDB);
            }
        }
    }

    public async Task CreateItemAsync(object itemJson)
    {
        var item = JsonSerializer.Deserialize<Item>(itemJson.ToString());
        if (item != null)
        {
            var itemCreate = new Item()
            {
                Name = item.Name,
                DateTimeCreated = DateTime.UtcNow,
            };
            await App.GlobalApp.RefItemRepository().CreateAsync(itemCreate);
        }
    }

    public async Task<Item?> ReadItemByIdAsync(int id)
    {
        await Task.Delay(1500); //fake load time
        var itemList = await App.GlobalApp.RefItemRepository().ReadAsync(filter: x => x.Id == id);
        var item = itemList.FirstOrDefault();
        return item;
    }

    public async Task<List<Item>> ReadItemListAsync()
    {
        await Task.Delay(1500); //fake load time
        var list = await App.GlobalApp.RefItemRepository().ReadAsync();
        return list;
    }

    private async void HWVControl_RawMessageReceived(object sender, HybridWebViewRawMessageReceivedEventArgs e)
    {
        Debug.WriteLine($@"RawMessageReceived: {e.Message}");
        await DisplayAlert("Raw Message Received", e.Message, "OK");
    }
}