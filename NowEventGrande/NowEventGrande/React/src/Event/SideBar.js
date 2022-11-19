import React from 'react';
import { Outlet, NavLink } from "react-router-dom";
import '../styles/sidebar.css';
import { useState } from "react";
import { useContext } from "react";
import { EventIdContext } from "./EventMain";

function SideBar() {
  const eventId = useContext(EventIdContext);

   const [Id, setEventId] = useState(eventId);

    return (
        <div className="vertical-menu">
          <ul>
            <li >
                <NavLink to={{pathname :`/event/${Id}/guests`}} state={{EventId: Id}}>Guest list</NavLink>
            </li>   
            <li >
              <NavLink to={{pathname :`/event/${Id}/budget`}} state={{EventId: Id}} >Budget</NavLink>
            </li>
            <li >
               <NavLink to={{pathname :`/event/${Id}/inspirations`}} state={{EventId: Id}} >Get inspired</NavLink>
            </li>
            <li >
              <NavLink to={{pathname :`/event/${Id}/location`}} state={{EventId: Id}} >Location and date</NavLink>
            </li>
            <li >
              <NavLink to={{pathname :`/event/${Id}/offer`}} state={{EventId: Id}} >Make offer</NavLink>
            </li>
            <li >
              <NavLink to={{pathname :`/event/${Id}/afterEvent`}} state={{EventId: Id}} >After event</NavLink>
            </li>
            <li >
              <NavLink to={{pathname :`/event/${Id}/summary`}} state={{EventId: Id}} >Summary</NavLink>
            </li>
          </ul>
          <Outlet />
        </div>
      );
}

export default SideBar;