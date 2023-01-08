import React from 'react';
import { useState, useEffect} from "react";
import { useNavigate, Link } from "react-router-dom";
import '../styles/tiles.css';
import emoticonsBanner from '../images/emoticonsBanner.jpg';
import { Nav } from "react-bootstrap"
import { ArrowRightCircle } from "react-bootstrap-icons";
import '../styles/banner.css';

function CreatedEvents(){
    const [events, setEvents] = useState([]);
    const user = localStorage.getItem('user');
    const navigate = useNavigate();

    useEffect(() => {
        fetchUserId();
    },[])

    async function GetUserEvents(result){
        const res = await fetch(`/events/GetEventsByUserId`, {
            Authorization: !user ? {} : { 'Authorization': 'Bearer ' + user.accessToken },
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(result)
        }); 
        res.json().then(res => setEvents(res));     
    }
    async function fetchUserId(){
        const res = await fetch('../account/GetCurrentUserId',{
            headers:{'Content-type':'application/json'},
            Authorization: !user ? {} : { 'Authorization': 'Bearer ' + user.accessToken }      
          });
          if (!res.ok) {
            const message = `An error has occured: ${res.status} - ${res.statusText}`;
            throw new Error(message);
          }
          else{       
            await res.text()
            .then((result)=> { 
                GetUserEvents(result);
            }) 
            .catch(res)       
          }
    }

    function goToEvent(evt){
            evt.preventDefault();
            const eventId = evt.currentTarget.value;
            navigate(`/event/${eventId}/summary`, {state: {EventId: eventId}});
    }
    return (
        <div className='event'> 
            {!user && <div className='notSignedInInfo'>You need to be signed in to see your events.
            </div>}   
            {user && events.length !== 0 &&     
            <div>
                <h2>Your current events:</h2>
                <div className="row longTileContainer">

                    {/* {Array.from(events).length !== 0 && */}
                    <div className="Event-col-3">
                    {Array.from(events).map((item, i) => {
                        return (
                            <form key={i}  >
                                <button className="longTile" value={item.id} onClick={goToEvent}>
                                <h2 className="longTileText">{item.name}</h2>
                                <p className="longTileTextSmall">{item.status}</p>
                                </button>
                            </form>)
                            })} 
                    </div>
                    {/* } */}
                

                    <div className="Event-col-2">
                        <img src={emoticonsBanner} alt="not loaded" className="verticalBanner"></img> 
                    </div>
                    
                </div>
            </div>    
            } 
            {user && events.length === 0 &&
            <div className="row longTileContainer">
                        <div className="Event-col-5">
                            <div className="createdEventsInfo">
                                Look like you don't have any events yet.
                                Click on the button below to create your first event.
                            </div> 
                            
                            <Nav.Link as={Link} to="/event">
                                <button className="createEventButton">Create event <ArrowRightCircle size={25} /></button>
                            </Nav.Link>
                        
                        </div> 
            </div>
            }  
        
        </div>
    )
}

export default CreatedEvents;