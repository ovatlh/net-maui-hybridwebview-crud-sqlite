using crudMauiApp.Data;
using crudMauiApp.Data.Entities;

namespace crudMauiApp;

public partial class MauiCrudPage : ContentPage
{
    public BaseRepository<Item> ItemRepository = new BaseRepository<Item>();
    public Item? Item;
    public bool IsUpdate;
    public MauiCrudPage()
    {
        InitializeComponent();
        InitAsync();
    }

    public async Task RefreshListAsync()
    {
        var list = await ItemRepository.ReadAsync();
        clxItem.ItemsSource = list;
    }

    public void RefreshBtnAction()
    {
        string btnText = IsUpdate ? "Save" : "Create";
        btnAction.Text = btnText;
    }

    public void RefreshForm()
    {
        string? entryText = Item != null ? Item.Name : string.Empty;
        txtName.Text = entryText;
    }

    public void ResetForm()
    {
        Item = null;
        RefreshForm();
        IsUpdate = false;
        RefreshBtnAction();
    }

    public async Task CreateItemAsync()
    {
        Item = new Item()
        {
            DateTimeCreated = DateTime.UtcNow,
            Name = txtName.Text
        };

        await ItemRepository.CreateAsync(Item);
        ResetForm();
        await RefreshListAsync();
    }

    public async Task UpdateItemAsync()
    {
        if (Item != null)
        {
            Item.Name = txtName.Text;
            await ItemRepository.UpdateAsync(Item);
            ResetForm();
            await RefreshListAsync();
        }
    }

    public async Task DeleteItemAsync()
    {
        if (Item != null)
        {
            await ItemRepository.DeleteAsync(Item);
            ResetForm();
            await RefreshListAsync();
        }
    }

    public async Task InitAsync()
    {
        await RefreshListAsync();
        ResetForm();
    }

    private async void btnAction_Clicked(object sender, EventArgs e)
    {
        if (IsUpdate)
        {
            await UpdateItemAsync();
        }
        else
        {
            await CreateItemAsync();
        }
    }

    private void btnUpdate_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        Item = (Item)button.BindingContext;
        IsUpdate = true;
        RefreshForm();
        RefreshBtnAction();
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        Item = (Item)button.BindingContext;
        await DeleteItemAsync();
    }
}