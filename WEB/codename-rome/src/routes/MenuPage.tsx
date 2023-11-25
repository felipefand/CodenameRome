import { useEffect, useState } from "react";
import Menu from "../components/Menu/Menu";
import Navbar from "../components/Navbar/Navbar";
import { MenuItem } from "../common/types/MenuItem";
import { getMenu, deleteMenu, postMenu } from "../common/connection/connection";

const MenuPage = () => {
    const [menu, setMenu] = useState<MenuItem[]>([])

    useEffect(() => {
        getMenu()
        .then(response => {if (response.ok) return response.json()})
        .then(newMenu => setMenu(newMenu))
    }, [])

    function addToMenu(item: MenuItem) {
        postMenu(item)
        .then(response => {if (response.ok) return response.json()})
        .then(newItem => setMenu((oldMenu => [...oldMenu, newItem])))
    }

    function deleteFromMenu(id: string){
        deleteMenu(id)
        .then(response => {if (response.ok) return response.json()})
        .then(removedItem => setMenu(oldMenu => oldMenu.filter(item => item.id !== removedItem.id)))
    }

    function updateMenu(item: MenuItem){
        console.log("Updating Menu")
    }

    return (
        <div>
            <Navbar/>
            <h1>Menu</h1>
            <Menu menu={menu} addToMenu={addToMenu} deleteFromMenu={deleteFromMenu}/>
        </div>
    )
}

export default MenuPage;