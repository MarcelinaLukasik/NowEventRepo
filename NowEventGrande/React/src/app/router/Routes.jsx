import { createBrowserRouter } from 'react-router-dom';
import App from "../../App";
import { Contact } from '../../componentPages/Contact';
import { ProductDetails } from "../../componentPages/OffersPage/ProductDetails";
import Event from '../../Event/Event';
import CreatedEvents from '../../Event/CreatedEvents';
import Guests from '../../Event/Guests';
import Budget from '../../Event/Budget';
import EventMain from '../../Event/EventMain';
import Offer from '../../Event/Offer';
import Location from '../../Event/Location';
import AfterEvent from '../../Event/AfterEvent';
import Summary from '../../Event/Summary';
import Details from '../../Event/Details';
import Home from "../../pages/Home";
import Offers from "../../pages/Offers";
import Registration from '../../pages/Registration';
import SignIn from '../../pages/SignIn';
import { OfferToPrepareEvent } from '../../componentPages/OffersPage/OfferToPrepareEvent';
import MessagesPanel from '../../Event/MessagesPanel';
import RequestMessage from '../../Event/RequestMessage';


export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <Home /> },
            { path: 'alloffers', element: <Offers /> },
            { path: 'alloffers/:id', element: <ProductDetails /> },
            { path: '/alloffers/offerToPrepare/:id', element: <OfferToPrepareEvent />},
            { path: '/offer/:requestId', element: <RequestMessage />},
            { path: 'registration', element: <Registration /> },
            { path: 'signIn', element: <SignIn /> },
            { path: 'contact', element: <Contact /> },
            { path: 'Event', element: <Event /> },
            { path: 'CreatedEvents', element: <CreatedEvents /> },
            { path: 'MessagesPanel', element: <MessagesPanel /> },
            { path: 'Event/:id/guests', element: <Guests /> },
            { path: 'Event/:id/budget', element: <Budget /> },
            { path: 'Event/:id/main', element: <EventMain /> },
            { path: 'Event/:id/offer', element: <Offer /> },
            { path: 'Event/:id/location', element: <Location /> },
            { path: 'Event/:id/afterEvent', element: <AfterEvent /> },
            { path: 'Event/:id/summary', element: <Summary /> },
            { path: 'Event/:id/details', element: <Details /> },
           
            

            /*           { path: '*', element: <Navigate replace to='/not-found' /> },*/
        ]
    }
])