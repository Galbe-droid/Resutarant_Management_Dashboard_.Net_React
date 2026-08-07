import {Login} from "./pages/Login.tsx";
import {AuthProvider} from "./Providers/AuthProvider.tsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {Dashboard} from "./pages/Dashboard.tsx";
import {DashboardLayout} from "./layouts/DashboardLayout.tsx";
import {SnackbarProvider} from "./Providers/SnackbarProvider.tsx";
import {TableDetails} from "./pages/tables/TableDetails.tsx";
import {ProductsDashboard} from "./pages/products/ProductsDashboard.tsx";
import {ProductDetails} from "./pages/products/ProductDetails.tsx";
import {CategoryDashboard} from "./pages/categories/CategoryDashboard.tsx";
import {CategoryDetails} from "./pages/categories/CategoryDetails.tsx";

function App() {

  return (
      <AuthProvider>
          <SnackbarProvider>
              <BrowserRouter>
                  <Routes>
                      <Route path="/" element={<Login />} />
                      <Route element={<DashboardLayout/>}>
                          <Route path="/dashboard" element={<Dashboard/>}/>
                          <Route path="/tables/:id" element={<TableDetails/>}/>
                          <Route path="/products" element={<ProductsDashboard/>}/>
                          <Route path="/products/:id" element={<ProductDetails/>}/>
                          <Route path="/categories" element={<CategoryDashboard/>}/>
                          <Route path="/categories/:id" element={<CategoryDetails/>}/>
                      </Route>
                  </Routes>
              </BrowserRouter>
          </SnackbarProvider>
      </AuthProvider>
  )
}

export default App
