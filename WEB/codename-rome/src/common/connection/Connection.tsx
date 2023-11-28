import { MenuItem } from "../types/MenuItem";

const MENU_URL = process.env.REACT_APP_API_URL

export function getMenu(){
    return fetch(`${MENU_URL}`)
}

export function postMenu(item: MenuItem){
    return fetch(`${MENU_URL}`, {
        method: "POST",
        body: JSON.stringify(item),
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

export function deleteMenu(id: string){
    return fetch(`${MENU_URL}/${id}`, {
        method: "DELETE",
    })
}

export function putMenu(item: MenuItem){
    return fetch (`${MENU_URL}/${item.id}`, {
        method: "PUT",
        body: JSON.stringify(item),
        headers: {
            'Content-Type': 'application/json'
        }
    })
}