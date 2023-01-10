import Axios from 'axios';

const SET_OFFERS = 'OFFERS.SET_OFFERS';
const SET_OFFERS_DETAILS = 'OFFERS.SET_OFFER_DETAILS';

const InitialState = {
    items: [],
    offersDetails: {
        id: 0,
        clientId: 0,
        contractorId: 0,
        name: '',
        size: '',
        date:'',
        eventStart:'',
        eventStart:'',
        theme: '',
        sizeRange: '',
        type: '',
        status: '',
        eventAddresses: '',
        guests: '',
        budgets: '',
        ratings: '',
    },
};

const reducer = (state = InitialState, action) => {
    const { type, data } = action;

    switch (type) {
        case SET_OFFERS: {
            return {
                ...state,
                items: data,
            };
        }
        case SET_OFFERS_DETAILS: {
            return {
                ...state,
                offersDetails: {
                    ...data,
                },
            };
        }
    }

    return state;
};

export default reducer;

export const OfferActions = {
    GetOffers: () => {
        return async (dispatch, getState) => {
            const response = await Axios.get(`/offer`);
            dispatch({
                type: SET_OFFERS,
                data: response.data,
            });
        };
    },
    GetOfferDetails: (id) => {
        return async (dispatch, getState) => {
            const response = await Axios.get(`/offer/${id}`);
            dispatch({
                type: SET_OFFERS_DETAILS,
                data: response.data,
            });
        };
    },
};