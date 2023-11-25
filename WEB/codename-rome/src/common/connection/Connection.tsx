import { MenuItem } from "../types/MenuItem";

const MENU_URL = process.env.REACT_APP_API_URL

export function getMenu(){
    let menu = fetch(`${MENU_URL}`)

    return menu;
}

export function postMenu(item: MenuItem){
    let result = fetch(`${MENU_URL}`, {
        method: "POST",
        body: JSON.stringify(item),
        headers: {
            'Content-Type': 'application/json'
        }
    })

    return result;
}

export function deleteMenu(id: string){
    let result = fetch(`${MENU_URL}/?id=${id}`, {
        method: "DELETE",
    })

    return result;
}

export function putMenu(item: MenuItem){
    let result = fetch (`${MENU_URL}`, {
        method: "PUT",
        body: JSON.stringify(item),
        headers: {
            'Content-Type': 'application/json'
        }
    })

    return result
}