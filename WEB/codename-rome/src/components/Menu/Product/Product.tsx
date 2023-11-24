import { MenuItem } from "../../../common/types/MenuItem";

const Product = (product : MenuItem) => {
    return (
        <div>
            <h2>{product.name}</h2>
            Descrição: {product.description} <br />
            Preço: {product.price}
        </div>
    )
}

export default Product;