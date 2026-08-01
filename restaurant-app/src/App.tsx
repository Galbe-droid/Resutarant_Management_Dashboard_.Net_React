import {Login} from "./pages/Login.tsx";
import {AuthProvider} from "./Providers/AuthProvider.tsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {Dashboard} from "./pages/Dashboard.tsx";
import {DashboardLayout} from "./layouts/DashboardLayout.tsx";
import {SnackbarProvider} from "./Providers/SnackbarProvider.tsx";
import {TableDetails} from "./pages/tables/TableDetails.tsx";

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
                      </Route>
                  </Routes>
              </BrowserRouter>
          </SnackbarProvider>
      </AuthProvider>
  )
}

export default App
