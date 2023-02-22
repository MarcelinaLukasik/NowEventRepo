import React from 'react';
import '../styles/guests.css';
import {Col} from "react-bootstrap";
import {useLocation} from 'react-router-dom';
import { useState , useEffect} from "react";

function Inspirations() {
    // const location = useLocation();
    // const eventId = location.state.EventId;
    // const [id, setEventId] = useState(eventId);
    const [res, setRes] = useState([]);

    useEffect(() => {
        handleFetch();
      }, []);

    async function handleFetch(){
        const key = await fetchKey();
        fetchRequest(key); 
    }

    async function fetchKey() {
        const res = await fetch(`/events/GetKey`);
        const dataJ = await res.text();
        const result = dataJ;
        return result;
        }

    async function fetchRequest(key)  {
        const data = await fetch(
          `https://api.unsplash.com/search/photos?page=1&query=birthday decor&client_id=${key}&per_page=30`
        );
        const dataJ = await data.json();
        const result = dataJ.results;
        console.log(result);
        setRes(result);
    };

    return (
        <div className="event">  
            <div>              
                <h1>Are you ready to get inspired?</h1>
                <div className="imagesColumns">
                    <div className="imagesColumn-1">
                    </div>
                    <div className="container-fluid">
                        <div className="imagesColumn-2">              
                            <br/>                                         
                            <div className="d-flex flex-wrap justify-content-center">
                                <div className="d-flex flex-column">
                                    {res.slice(0,10).map((val) => {
                                        return (
                                        <>
                                            <img
                                            key={val.id}
                                            className="img-fluid"
                                            src={val.urls.small}
                                            alt="val.alt_description"
                                            loading="lazy"
                                            />
                                        </>
                                        );
                                    })}
                                </div> 
                                <div className="d-flex flex-column">
                                    {res.slice(10,20).map((val) => {
                                        return (
                                        <>
                                            <img
                                            key={val.id}
                                            className="img-fluid"
                                            src={val.urls.small}
                                            alt="val.alt_description"
                                            loading="lazy"
                                            />
                                        </>
                                        );
                                    })}
                                </div> 
                                <div className="d-flex flex-column">
                                    {res.slice(20).map((val) => {
                                        return (
                                        <>
                                            <img
                                            key={val.id}
                                            className="img-fluid"
                                            src={val.urls.small}
                                            alt="val.alt_description"
                                            loading="lazy"
                                            />
                                        </>
                                        );
                                    })}
                                </div>                         
                            </div>
                        </div>
                    </div>            
                </div>                             
            </div>
        </div>
)
}

export default Inspirations;