import axios from "axios";
// Central axios agent for REST

// Default url for axios
axios.defaults.baseURL = 'https://localhost:7267/';

const responseBody = (response) => response.data;

const request = {
    get: (url) => axios.get(url).then(responseBody),
    post: (url, body) => axios.post(url, body).then(responseBody),
    put: (url, body) => axios.put(url, body).then(responseBody),
    delete: (url) => axios.delete(url).then(responseBody),
    }

const Offers = {
    productsList: () => request.get('offers'),
    productDetails: (id) => request.get(`offers/${id}`)
}

const agent = {
    Offers
}

export default agent;
