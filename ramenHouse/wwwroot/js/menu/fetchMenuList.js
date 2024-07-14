const loadingEl = document.getElementById("menu-loading")
const menuList = document.getElementById("menu-content-container")
//loading indicator container is set to be flexbox for centering the indicator 
loadingEl.style.display = "flex"
menuList.style.display = "none"
fetch(`/menu/GetMenuList/${category}`)
    .then(response => response.text())
    .then((html) => {
        menuList.innerHTML = html
        const menuImgs = menuList.querySelectorAll("img")
        const totalImgs = menuImgs.length
        let loadedImgCounts = 0
        if (totalImgs === 0) {
            loadingEl.style.display = "none"
            menuList.style.display = "block"
        }
        else {

            for (imgEl of menuImgs) {
                imgEl.addEventListener("load", () => {
                    loadedImgCounts += 1
                    if (loadedImgCounts === totalImgs) {
                        loadingEl.style.display = "none"
                        menuList.style.display = "block"
                    }
                })
            }
        }


    })