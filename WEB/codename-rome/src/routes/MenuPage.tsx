import { useEffect, useState } from "react";
import Menu from "../components/Menu/Menu";
import Navbar from "../components/Navbar/Navbar";
import { MenuItem } from "../common/types/MenuItem";
import { getMenu, deleteMenu, postMenu, putMenu } from "../common/connection/connection";

const MenuPage = () => {
    const [menu, setMenu] = useState<MenuItem[]>([])

    useEffect(() => {
        getMenu()
        .then(response => response.json())
        .then(newMenu => setMenu(newMenu))
        .catch((err) => console.log(err))
    }, [])

    function addToMenu(item: MenuItem) {
        postMenu(item)
        .then(response => response.json())
        .then(newItem => setMenu((oldMenu => [...oldMenu, newItem])))
        .catch((err) => console.log(err))
    }

    function deleteFromMenu(id: string){
        deleteMenu(id)
        .then(response => response.json())
        .then(removedItem => setMenu(oldMenu => oldMenu.filter(item => item.id !== removedItem.id)))
        .catch((err) => console.log(err))
    }

    function updateMenu(item: MenuItem){
        putMenu(item)
        .then(response => response.json())
        .then(updatedItem => setMenu(oldMenu => {
            let product = oldMenu.find((p) => p.id === updatedItem.id)
            if (product) Object.assign(product, updatedItem)
            return [...oldMenu]
        }))
        .catch((err) => console.log(err))
    }

    return (
        <div>
            <Navbar/>
            <h1>Menu</h1>
            <Menu menu={menu} addToMenu={addToMenu} deleteFromMenu={deleteFromMenu} updateMenu={updateMenu}/>
        </div>
    )
}

export default MenuPage;