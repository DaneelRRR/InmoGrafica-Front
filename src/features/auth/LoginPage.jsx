import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import client from '../../api/axiosClient';
import { useAuth } from '../../context/AuthContext';
import Button from '../../components/Button';

export default function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    const { login } = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setLoading(true);
        try {
            const response = await client.post('/auth/login', { email, password });

            const { usuario, token } = response.data;

            login(usuario, token);
            navigate('/inmuebles');
        } catch (err) {
            console.error(err);
            setError('Credenciales incorrectas o error de servidor.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100">

            {/* Tarjeta del Login */}
            <div className="bg-white p-8 rounded-xl shadow-2xl w-full max-w-md border border-gray-200">

                <div className="text-center mb-8">
                    <h1 className="text-3xl font-extrabold text-blue-600 tracking-tighter">Inmo<span className="text-gray-800">Gráfica</span></h1>
                    <p className="text-gray-500 mt-2 text-sm">Ingresa a tu panel de gestión</p>
                </div>

                {error && (
                    <div className="bg-red-50 text-red-600 p-3 rounded mb-4 text-sm text-center border border-red-100">
                        {error}
                    </div>
                )}

                <form onSubmit={handleSubmit} className="space-y-6">
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Correo Electrónico</label>
                        <input
                            type="email"
                            required
                            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition"
                            placeholder="admin@inmo.com"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>

                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">Contraseña</label>
                        <input
                            type="password"
                            required
                            className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition"
                            placeholder="••••••"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>

                    <Button
                        variant="primary"
                        className="w-full py-3 text-lg"
                        disabled={loading}
                    >
                        {loading ? 'Ingresando...' : 'Iniciar Sesión'}
                    </Button>
                </form>

                <div className="mt-6 text-center text-xs text-gray-400">
                    © 2026 InmoGráfica System
                </div>
            </div>
        </div>
    );
}