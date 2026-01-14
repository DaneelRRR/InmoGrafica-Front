import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import client from '../../api/axiosClient';

export default function CrearInmueble() {
  const [form, setForm] = useState({ nombre: '', direccion: '' });
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await client.post('/Inmueble', form);
      navigate('/inmuebles');
    } catch (error) {
      alert("Error creando el inmueble");
    }
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 bg-white rounded shadow-lg">
      <h2 className="text-2xl font-bold mb-6">Registrar Nuevo Bien</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-4">
          <label className="block text-gray-700 font-bold mb-2">Nombre del Bien</label>
          <input 
            type="text" 
            className="w-full border p-2 rounded" 
            placeholder="Ej: Casa en la Playa"
            value={form.nombre}
            onChange={e => setForm({...form, nombre: e.target.value})}
            required
          />
        </div>
        <div className="mb-6">
          <label className="block text-gray-700 font-bold mb-2">Dirección</label>
          <input 
            type="text" 
            className="w-full border p-2 rounded" 
            placeholder="Ej: Av. Costanera 123"
            value={form.direccion}
            onChange={e => setForm({...form, direccion: e.target.value})}
            required
          />
        </div>
        <button className="w-full bg-green-600 text-white font-bold py-2 rounded hover:bg-green-700">
          Guardar Inmueble
        </button>
      </form>
    </div>
  );
}