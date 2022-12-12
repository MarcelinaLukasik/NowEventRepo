import React from 'react';
import { useState, useEffect} from "react";

function CreatedEvents(){
    const [events, setEvents] = useState([]);
    const user = localStorage.getItem('user');

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


    return (
        <div className='event'> 
        {!user && <div>You need to be signed in to see your events.</div>}    
        {events.length !== 0 &&     
        <div>
             <div>Your current events:</div>
            {Array.from(events).map((item, i) => {
                return (
                    <div key={i}>
                        <div>{item.name}</div>
                        <div>{item.status}</div>
                    </div>)
                    })} 
        </div>
        }       
        </div>
    )
}

export default CreatedEvents;