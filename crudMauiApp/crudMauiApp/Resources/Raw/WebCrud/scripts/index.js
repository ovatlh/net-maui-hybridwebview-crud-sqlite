let isUpdate = false;
let item = {
    Id: 0,
    Name: "",
    DateTimeCreated: null
};

function formToObject(formElement) {
    const obj = Object.fromEntries(new FormData(formElement));

    Object.keys(obj).forEach(function (key) {
        const value = obj[key];
        obj[key] = typeof value === "string" && !isNaN(value) ? Number(value) : value;
    });

    return obj;
}

function objectToForm(formId, data) {
    const form = document.getElementById(formId);

    if (!form) {
        return;
    }

    for (const key in data) {
        if (data.hasOwnProperty(key)) {
            const input = form.querySelector(`[id="input.${key}"]`);
            if (input) {
                if (input.type == "checkbox") {
                    input.checked = data[key];
                } else {
                    input.value = data[key];
                }
            }
        }
    }
}

function toggleScreenLoading(isActive = false) {
    const el = document.getElementById("screen-loading");
    el.classList.remove("active");
    if (isActive) {
        el.classList.add("active");
    }
}

async function RefreshItemListAsync() {
    toggleScreenLoading(true);
    let listHTML = "<p>No items</p>";

    const list = await window.HybridWebView.InvokeDotNet("ReadItemListAsync");
    if (list.length > 0) {
        listHTML = list.reduce((result, item) => {
            const itemHTML = `
                <div class="item">
                    <p>${item.Name}</p>
                    <div class="item-actions">
                        <button onclick="btnUpdateItemAsync(${item.Id})">Update</button>
                        <button onclick="btnDeleteItemAsync(${item.Id})">Delete</button>
                    </div>
                </div>
            `;
            return result + itemHTML;
        }, "");
    }

    document.getElementById("item-list-container").innerHTML = listHTML;
    toggleScreenLoading(false);
}

function RefreshBtnAction() {
    let btnText = isUpdate ? "Save" : "Create";
    document.getElementById("btn.action").innerHTML = btnText;
}

function RefreshForm() {
    document.getElementById("input.Name").value = item.Name;
    document.getElementById("input.Id").value = item.Id;
}

function ResetForm() {
    item = {
        Id: 0,
        Name: "",
        DateTimeCreated: null
    };
    document.getElementById("formItem").reset();
    RefreshForm();
    isUpdate = false;
    RefreshBtnAction();
}

async function formItemSubmit(event) {
    event.preventDefault();

    const data = formToObject(event.srcElement);
    item = data;
    if (isUpdate) {
        await UpdateItemAsync();
    } else {
        await CreateItemAsync();
    }
}

async function btnUpdateItemAsync(id = 0) {
    toggleScreenLoading(true);

    const data = await window.HybridWebView.InvokeDotNet("ReadItemByIdAsync", [id]);
    if (data != null) {
        item = data;
        isUpdate = true;
    }
    RefreshForm();
    RefreshBtnAction();

    toggleScreenLoading(false);
}

async function btnDeleteItemAsync(id = 0) {
    await DeleteItemAsync(id);
}

async function CreateItemAsync() {
    toggleScreenLoading(true);

    await window.HybridWebView.InvokeDotNet("CreateItemAsync", [item]);
    ResetForm();
    await RefreshItemListAsync();
}

async function UpdateItemAsync() {
    toggleScreenLoading(true);

    await window.HybridWebView.InvokeDotNet("UpdateItemAsync", [item]);
    ResetForm();
    await RefreshItemListAsync();
}

async function DeleteItemAsync(id = 0) {
    toggleScreenLoading(true);

    await window.HybridWebView.InvokeDotNet("DeleteItemByIdAsync", [id]);
    ResetForm();
    await RefreshItemListAsync();
}

async function InitAsync() {
    toggleScreenLoading(true);

    await RefreshItemListAsync();
    ResetForm();
}

InitAsync();