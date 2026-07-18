import {Login} from "./pages/Login.tsx";
import {AuthProvider} from "./Providers/AuthProvider.tsx";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {Dashboard} from "./pages/Dashboard.tsx";
import {DashboardLayout} from "./layouts/DashboardLayout.tsx";

function App() {

  return (
      <AuthProvider>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route element={<DashboardLayout/>}>
                    <Route path="/dashboard" element={<Dashboard/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
      </AuthProvider>
  )
}

export default App
