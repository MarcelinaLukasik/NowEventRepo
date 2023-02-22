import React from 'react';
import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import { RouterProvider, createBrowserRouter, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Layout from "./pages/Layout";
import Contact from "./pages/Contact";
import Home from "./pages/Home";
import FetchApi2 from './FetchApi/FetchApi2';
import Event from './Event/Event';
import Guests from './Event/Guests';
import Budget from './Event/Budget';
import EventMain from './Event/EventMain';
import Inspirations from './Event/Inspirations';
import Offer from './Event/Offer';
import Location from './Event/Location';
import AfterEvent from './Event/AfterEvent';
import Summary from './Event/Summary';
import Details from './Event/Details';
import CreatedEvents from './Event/CreatedEvents';
import Offers from './pages/Offers';
import Registration from './pages/Registration';
import SignIn from './pages/SignIn';
import { ProductDetails } from './componentPages/OffersPage/ProductDetails';
import App from './App';

const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <Home /> },
            { path: 'alloffers', element: <Offers /> },
            { path: 'alloffers/:id', element: <ProductDetails /> },
            /*           { path: '*', element: <Navigate replace to='/not-found' /> },*/
        ]
    }
])

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
);



    //return (
    //    <RouterProvider>
    //        <Routes>
    //            <Route path="/" element={<Layout />}>
    //                <Route index element={<Home />} />
    //                {/*<Route path="/registration" element={<Registration />} />*/}
    //                {/*<Route path="/signIn" element={<SignIn />} />*/}
    //                {/*<Route path="/inspirations" element={<Inspirations />} />*/}
    //                {/*<Route path="contact" element={<Contact />} />*/}
    //                {/*<Route path="Event" element={<Event />} />*/}
    //                {/*<Route path="CreatedEvents" element={<CreatedEvents />} />*/}
    //                {/*<Route path="user2" element={<FetchApi2 />} />*/}
    //                <Route path="offers" element={<Offers />} />
    //                <Route path="/offers/:id" element={<ProductDetails />} />
    //                {/*<Route path="/Event/:id/guests" element={<Guests />} />*/}
    //                {/*<Route path="/Event/:id/budget" element={<Budget />} />*/}
    //                {/*<Route path="/Event/:id/main" element={<EventMain />} />*/}
    //                {/*<Route path="/Event/:id/offer" element={<Offer />} />*/}
    //                {/*<Route path="/Event/:id/location" element={<Location />} />*/}
    //                {/*<Route path="/Event/:id/details" element={<Details />} />*/}
    //                {/*<Route path="/Event/:id/afterEvent" element={<AfterEvent />} />*/}
    //                {/*<Route path="/Event/:id/summary" element={<Summary />} />*/}
    //            </Route>
    //        </Routes>
    //    </RouterProvider>
    //);
// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

