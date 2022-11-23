import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Layout from "./pages/Layout";
import Contact from "./pages/Contact";
import Home from "./pages/Home";
import FetchApi from './FetchApi/FetchApi';
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
import Offers from './pages/Offers';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <AllRoutes />
    </React.StrictMode>
);

function AllRoutes() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Home />} />
                    <Route path="/inspirations" element={<Inspirations />} />
                    <Route path="contact" element={<Contact />} />
                    <Route path="Event" element={<Event />} />
                    <Route path="user" element={<FetchApi />} />
                    <Route path="user2" element={<FetchApi2 />} />
                    <Route path="/offers" element={<Offers />} />
                    <Route path="/Event/:id/guests" element={<Guests />} />
                    <Route path="/Event/:id/budget" element={<Budget />} />
                    <Route path="/Event/:id/main" element={<EventMain />} />
                    <Route path="/Event/:id/offer" element={<Offer />} />
                    <Route path="/Event/:id/location" element={<Location />} />
                    <Route path="/Event/:id/afterEvent" element={<AfterEvent />} />
                    <Route path="/Event/:id/summary" element={<Summary />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

//testing
