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

//toggle new allergy form 

createAllergybtn = document.getElementById("create-allergy-toggle")
createNewAllergyModel = document.getElementById("create-new-allergy-form")

createAllergybtn.addEventListener("click", () => {
    if (createNewAllergyModel.style.height === "48px") {
        createNewAllergyModel.style.height = "0px";
    } else {
        createNewAllergyModel.style.height = "48px";
    }
});


//send http request on creating new allergy
const createAllergyForm = document.getElementById("create-allergy-form")
createAllergyForm.addEventListener("submit", submitCreateAllergyFrom)

function submitCreateAllergyFrom(e) {
    const formData = new FormData(this)
    e.preventDefault()
    fetch("/admin/allergyCreate", {
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