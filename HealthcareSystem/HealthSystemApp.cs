using System;
using System.Collections.Generic;

namespace HealthcareSystem
{
    public class HealthSystemApp
    {
        private readonly Repository<Patient> _patientRepo = new Repository<Patient>();
        private readonly Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
        private readonly Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();

        public void SeedData()
        {
            // Add patients
            _patientRepo.Add(new Patient(1, "John Doe", 35, "Male"));
            _patientRepo.Add(new Patient(2, "Jane Smith", 28, "Female"));
            _patientRepo.Add(new Patient(3, "Robert Johnson", 45, "Male"));

            // Add prescriptions
            _prescriptionRepo.Add(new Prescription(101, 1, "Ibuprofen", DateTime.Now.AddDays(-10)));
            _prescriptionRepo.Add(new Prescription(102, 1, "Amoxicillin", DateTime.Now.AddDays(-5)));
            _prescriptionRepo.Add(new Prescription(103, 2, "Paracetamol", DateTime.Now.AddDays(-3)));
            _prescriptionRepo.Add(new Prescription(104, 3, "Lisinopril", DateTime.Now.AddDays(-1)));
            _prescriptionRepo.Add(new Prescription(105, 3, "Atorvastatin", DateTime.Now));
        }

        public void BuildPrescriptionMap()
        {
            _prescriptionMap.Clear();
            foreach (var prescription in _prescriptionRepo.GetAll())
            {
                if (!_prescriptionMap.ContainsKey(prescription.PatientId))
                    _prescriptionMap[prescription.PatientId] = new List<Prescription>();
                _prescriptionMap[prescription.PatientId].Add(prescription);
            }
        }

        public List<Prescription> GetPrescriptionsByPatientId(int patientId) =>
            _prescriptionMap.TryGetValue(patientId, out var prescriptions) ? prescriptions : new List<Prescription>();

        public void PrintAllPatients()
        {
            Console.WriteLine("\nAll Patients:");
            _patientRepo.GetAll().ForEach(Console.WriteLine);
        }

        public void PrintPrescriptionsForPatient(int patientId)
        {
            var patient = _patientRepo.GetById(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine($"Patient with ID {patientId} not found.");
                return;
            }

            Console.WriteLine($"\nPrescriptions for Patient {patient.Name} (ID: {patientId}):");
            var prescriptions = GetPrescriptionsByPatientId(patientId);
            Console.WriteLine(prescriptions.Count == 0 ? "No prescriptions found." : string.Join("\n", prescriptions));
        }
    }
}