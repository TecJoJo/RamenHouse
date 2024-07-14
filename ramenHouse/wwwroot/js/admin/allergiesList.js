const grid = $('#allergies-management').guiGrid({
    columns: [
       
        {
            header: "Allergy Name",
            field: "name"
        },
        {
            header: "Allergy Abbr.",
            field: "abbreviation"
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
    onSourceEdit: () => {console.log("list is edit!!!") },
});



createAllergybtn = document.getElementById("create-allergy-toggle")
createNewAllergyModel = document.getElementById("create-new-allergy-model")

createAllergybtn.addEventListener("click", () => {
    if (createNewAllergyModel.style.display === "block") {
        createNewAllergyModel.style.display = "none";
    } else {
        createNewAllergyModel.style.display = "block";
    }
});