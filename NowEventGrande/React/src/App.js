import React from 'react';
import { Outlet } from 'react-router-dom';
import { Container } from '@mui/material';
import Layout from './pages/Layout';

export default function App () {
    return(
        <>
            <Layout />
            <Outlet />
        </>
  );
}