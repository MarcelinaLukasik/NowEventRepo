import React from 'react';
import { useState, useEffect} from "react";
import { useNavigate, Link } from "react-router-dom";
import '../styles/tiles.css';
import emoticonsBanner from '../images/emoticonsBanner.jpg';
import { Nav } from "react-bootstrap"
import { ArrowRightCircle } from "react-bootstrap-icons";
import '../styles/banner.css';

function PostedEvents(){
    const [offers, setOffers] = useState([]);
    const user = localStorage.getItem('user');

    useEffect(() => {
        fetchUserId();
    },[])

    async function GetUserOffers(result){
        const res = await fetch(`/offer/GetOffersByUserId`, {
            Authorization: !user ? {} : { 'Authorization': 'Bearer ' + user.accessToken },
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(result)
        }); 
        res.json().then(res => setOffers(res));     
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
                GetUserOffers(result);
            }) 
            .catch(res)       
          }
    }

    return (
        <div className='event'> 
            {!user && <div className='notSignedInInfo'>You need to be signed in to see your posted events.
            </div>}   
            {user && offers.length !== 0 &&     
            <div>
                <h2>Your posted events:</h2>
                <div className="row longTileContainer">
                    <div className="Event-col-3">
                    {Array.from(offers).map((item, i) => {
                        return (
                            <form key={i}  >
                                <button className="longTile" value={item.name}>
                                <h2 className="longTileText">{item.name}</h2>
                                <p className="longTileTextSmall">{item.status}</p>
                                </button>
                            </form>)
                            })} 
                    </div>            

                    <div className="Event-col-2">
                        <img src={emoticonsBanner} alt="not loaded" className="verticalBanner"></img> 
                    </div>
                    
                </div>
            </div>    
            } 
            {user && offers.length === 0 &&
            <div className="row longTileContainer">
                        <div className="Event-col-5">
                            <div className="createdEventsInfo">
                                Look like you don't have any offers yet.
                                Complete your event and use "Post offer" button.
                            </div>                        
                        </div> 
            </div>
            }          
        </div>
    )
}

export default PostedEvents;