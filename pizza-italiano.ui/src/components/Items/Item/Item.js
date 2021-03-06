import { NavLink } from 'react-router-dom';
import useAuth from '../../../hooks/useAuth';
import { addItemToCart } from '../../Cart/Cart';
import { warning } from '../../notifications';
import styles from './Item.module.css'

function Item(props) {
    const [auth] = useAuth();

    const onClickHandler = (item) => {
        addItemToCart(item);
        warning(`Added item '${item.name}'`, true);
        setTimeout({}, 1000);
        window.location.reload();
    }

    return (
        <div className={`card ${styles.itemBorder} ms-2 mt-2`}>
            <div className="card-body">
               <div className="row">
                    <div className="col-4">
                        <div className="row">
                            <div className="col">
                                <h4>{props.name}</h4>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col mt-5">
                                <p>Cost:</p>
                                <h4>{props.cost} USD</h4>
                            </div>
                        </div>
                    </div>
                    {auth ? 
                        <div className='text-end'>
                            <div>
                                <button className={`btn btn-primary mt-2 px-5`} style={{width: '14rem'}} onClick={onClickHandler.bind(this, props)} >
                                    Add to cart
                                </button>
                            </div>
                            {auth.role === 'admin' ?
                                <div>
                                    <NavLink to={`/products/details/${props.id}`} className={"btn btn-warning mt-2 px-5"}>
                                        Details
                                    </NavLink>
                                </div>
                            : null}
                        </div>
                    : null}
                </div>
            </div>
        </div>
    );
}

export default Item;