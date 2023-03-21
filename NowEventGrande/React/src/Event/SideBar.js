import React from 'react';
import { Outlet, NavLink } from "react-router-dom";
import '../styles/sidebar.css';

function SideBar(props) {
    return (
        <div className="vertical-menu">
          <ul>       
            <li >
                <NavLink to={{pathname :`/event/${props.eventId}/guests`}} state={{EventId: props.eventId}}>Guest list</NavLink>
            </li>   
            <li >
              <NavLink to={{pathname :`/event/${props.eventId}/budget`}} state={{EventId: props.eventId}} >Budget</NavLink>
            </li>
            <li >
              <NavLink to={{pathname :`/event/${props.eventId}/location`}} state={{EventId: props.eventId}} >Location and date</NavLink>
            </li>
            <li >
              <NavLink to={{pathname :`/event/${props.eventId}/offer`}} state={{EventId: props.eventId}} >Make offer</NavLink>
            </li>        
            <li >
              <NavLink to={{pathname :`/event/${props.eventId}/afterEvent`}} state={{EventId: props.eventId}} >After event (optional)</NavLink>
            </li>
            <li >
              <NavLink to={{pathname :`/event/${props.eventId}/details`}} state={{EventId: props.eventId}} >Details (optional)</NavLink>
            </li>
            <li >
              <NavLink to={{pathname :`/event/${props.eventId}/summary`}} state={{EventId: props.eventId}} >Summary</NavLink>
            </li>
          </ul>
          <Outlet />
        </div>
      );
}

export default SideBar;