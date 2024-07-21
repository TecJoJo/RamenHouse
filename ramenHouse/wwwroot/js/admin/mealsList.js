



const grid = $('#meals-management').guiGrid({
    columns: [
        {
            header: "Meal ID",
            field: "mealId"
        },
        {
            header: "Dish Name",
            field: "dishName"
        },
        {
            header: "Description",
            field: "description"
        },
        {
            header: "Image URL",
            field: "imageUrl"
        },
        {
            header: "Rating",
            field: "rating"
        },
        {
            header: "Allergies",
            field: "allergies",
            width:200
        },
        {
            header: "Base Price",
            field: "basePrice"
        },
        {
            header: "Discount",
            field: "discount"
        },
        {
            header: "Sale Price",
            field: "salePrice"
        },
        {
            header: "Featured Meal",
            field: "isFeatured"
        },
        {
            header: '',
            field: 'deleteId',

            view: function (value) {
                return `
    <button onClick="deleteMeal(${value})" type="button" class="rounded-full border bg-red-900 px-4 py-2 text-white hover:bg-red-700">Delete</button>
    `
            },
            cellEditing: {
                enabled: false
            },
        }

    ],
    source: source,
    cellEditing: true,
    onSourceEdit: editMeal,
});


function editMeal(e) {

    console.log("the source is edit", e.after)
    console.log("the source is edit", e.before)

    const data = e.after

    //apperently Generic Ui can only detect numbers and strings
    //so we need to covert some special field into their type

    // Ensure isFeatured is sent as a boolean
    if (data.isFeatured === "true") {
        data.isFeatured = true;
    }
    if (data.isFeatured === "false") {
        data.isFeatured = false;
    }

    // convert the discount to right decimal number 
    data.discount = Number(data.discount).toFixed(2)


    fetch("/Admin/MealUpdate", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    }).then((response) => {
        if (response.ok) window.location.reload()
        else console.log("server is not happy with the request!!!")

    }) // noticiing I inteneded omitted the response.json() since it is not necessary
    //because i know the backend is not going to return anything
        .catch(error => console.log(error))

   

    //console.log("grid", grid)
    //const newSource = source.map((meal) => {
    //    if (meal.mealId === e.after.mealId) {
    //        mealModified = e.after
    //        mealModified.salePrice = mealModified.basePrice * (1 - mealModified.discount)
    //        return mealModified
    //    }
    //    return meal
    //});
    //grid.setSource(newSource)

}

function deleteMeal(deleteId) {
    console.log("meal' Id to be delete", deleteId);


    fetch("/Admin/MealDelete", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(deleteId)
    })
        .then(response => response.json()) // Parse the JSON from the response
        .then(data => {
            if (data.success) {
                location.reload(); // Refresh the page
            } else {
                // Handle error
                console.error(data.message);
            }
        })
        .catch(error => console.error('Error:', error));
}
