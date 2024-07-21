const grid = $('#categories-management').guiGrid({
    columns: [

        {
            header: "Category Name",
            field: "name"
        },
        {
            header: "Category description",
            field: "description"
        },

        {
            header: '',
            field: 'deleteId',

            view: function (value) {
                return `
    <button onClick="deleteCategory(${value})" type="button" class="rounded-full border bg-red-900 px-4 py-2 text-white hover:bg-red-700">Delete</button>
    `
            },
            cellEditing: {
                enabled: false
            },
        }

    ],
    source: source,
    cellEditing: true,
    onSourceEdit: editCategory,
});


createCategorybtn = document.getElementById("create-category-toggle")
createNewCategoryModel = document.getElementById("create-new-category-form")

createCategorybtn.addEventListener("click", () => {
    if (createNewCategoryModel.style.height === "48px") {
        createNewCategoryModel.style.height = "0px";
    } else {
        createNewCategoryModel.style.height = "48px";
    }
});


//send http request on creating new Category
const createCategoryForm = document.getElementById("create-category-form")
createCategoryForm.addEventListener("submit", submitCreateCategoryFrom)

function submitCreateCategoryFrom(e) {
    const formData = new FormData(this)
    e.preventDefault()
    fetch("/admin/CategoryCreate", {
        method: "POST",
       
        body: formData,
    }).then((response) => {
        if (response.ok) {
            //as i am creating backend on my own, i know backend will return 
            //json data wrapping the validation info
            response.json().then((res) => {
                console.log(res)
                window.location.reload()
            })

        }
        else {
            //we can display some error message here

            console.log("backend error!!!")
        }
    }).catch(
        error => console.log("Error!!!", error)
    )
}


function editCategory(e) {

    console.log("the source is edit", e.after)
    console.log("the source is edit", e.before)

    const data = e.after




    fetch("/Admin/CategoryUpdate", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    }).then((response) => {
        if (response.ok) {
            //as i am creating backend on my own, i know backend will return 
            //json data wrapping the validation info
            response.json().then((res) => {
                console.log(res)
                window.location.reload()
            })

        }
        else {
            //we can display some error message here

            console.log("backend error!!!")
        }
    }).catch(
        error => console.log("Error!!!", error)
    )
}

function deleteCategory(deleteId) {
    fetch("/admin/CategoryDelete", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(deleteId),
    }).then((response) => {
        if (response.ok) {
            //as i am creating backend on my own, i know backend will return 
            //json data wrapping the validation info
            response.json().then((res) => {
                console.log(res)
                window.location.reload()
            })

        }
        else {
            //we can display some error message here

            console.log("backend error!!!")
        }
    }).catch(
        error => console.log("Error!!!", error)
    )
}
