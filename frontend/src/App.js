import React from 'react';
import "./App.css";
import Register from './Components/Register';
import Login from './Components/Login';
import { Navigate } from 'react-router-dom';


import { BrowserRouter, Routes, Route } from "react-router-dom";
import CreateProduct from './Owner/CreateProduct';
import ProductsList from './Tenant/ProductDetails';
import UserProducts from './Owner/UserProducts';
import ErrorPage from './Components/ErrorPage';

function App() {
  return (
   <div>
    <BrowserRouter>
      <Routes>
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="createproduct" element={<CreateProduct />} />
          <Route path="productlist" element={<ProductsList />} />
          <Route path="userproducts" element={<UserProducts />} />
          <Route path="*" element={<Navigate to="/login" replace />} />
          <Route path="error" element={<ErrorPage/>} />

        {/* <Route path="*" element={<Navigate to="/user/login" replace />} /> */}
         </Routes>
    </BrowserRouter>
   </div>
  );
}

export default App;
