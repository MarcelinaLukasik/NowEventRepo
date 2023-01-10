import { OffersMainPage } from "../componentPages/OffersPage/OffersMainPage";
import { motion } from 'framer-motion'
import { ProductDetails } from "../componentPages/OffersPage/ProductDetails";
import { Routes, Route } from 'react-router-dom';
function Offers() {
    return (
        <motion.div layout>
            <Routes>
                <Route exact path={'/offer'} component={OffersMainPage} />
                <Route exact path={'/offer:id'} component={ProductDetails} />
            </Routes>
        </motion.div>
    )
}

export default Offers;