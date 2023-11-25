import { MenuItem } from "../../../common/types/MenuItem";
import "./Product.scss";

interface ProductProps {
    product: MenuItem,
    deleteFromMenu: (id: string) => void
}

const Product = ({product, deleteFromMenu} : ProductProps) => {
    return (
        <div className="card">
            <div className="product-title">{product.name}</div>
            <div className="product-description">{product.description}</div>
            <div className="product-price">$ {product.price}</div>
            <button onClick={() => product.id? deleteFromMenu(product.id) : ""}>delete</button>
        </div>
    )
}

export default Product;