export function getMenu(){
    let menu = fetch(`${process.env.REACT_APP_API_URL}`)
    .then(response => response.json())

    return menu;
}